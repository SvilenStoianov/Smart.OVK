using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LinqToDB.Data;

namespace Smart.Web.Infrastructure
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GatewayProtocol
    {
        public static JsonSerializerSettings SerializerSettings { get; } = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static async Task<GatewayResult> ProcessGatewayMessage(
            GatewayMessage request,
            HandlerCollection handlers,
            IServiceProvider serviceProvider)
        {
            if (request == null)
            {
                return GatewayResult.FromErrorMessage("The request body is empty.");
            }

            if (string.IsNullOrWhiteSpace(request.MessageType))
            {
                return GatewayResult.FromErrorMessage("The request MessageType is empty.");
            }

            if (string.IsNullOrWhiteSpace(request.MessageJson))
            {
                return GatewayResult.FromErrorMessage("The request MessageJson is empty.");
            }

            try
            {
                var handler = handlers.GetHandler(request.MessageType);

                if (handler == null)
                {
                    return GatewayResult.FromErrorMessage("Няма Handler, който да обработи заявка с тип " + request.MessageType);
                }

                var context = serviceProvider.GetService<IHttpContextAccessor>().HttpContext;

                var session = context.GetRequestSession();

                if (handler.RequireAuthentication && !session.IsAuthenticated)
                {
                    return GatewayResult.FromErrorMessage("Нямате права да достъпвате този ресурс: " + request.MessageType);
                }

                var handlerInstance = serviceProvider.GetService(handler.Method.DeclaringType);

                var requestModel = JsonConvert.DeserializeObject(request.MessageJson, handler.RequestType, SerializerSettings);

                var (isValid, validationErrorMessages) = DataValidator.Validate(requestModel);

                if (!isValid)
                {
                    return GatewayResult.FromErrorMessages(validationErrorMessages);
                }

                var parameters = handler.Method.GetParameters()
                        .Select(info =>
                        {
                            if (info.ParameterType == handler.RequestType)
                            {
                                return requestModel;
                            }

                            if (info.ParameterType == typeof(GatewayMessage))
                            {
                                return request;
                            }

                            if (info.ParameterType == typeof(RequestSession))
                            {
                                return session;
                            }

                            throw new NotSupportedException(
                                string.Format("Parameters don't match for handler method {0}.{1}",
                                              handler.Method.DeclaringType.Name, handler.Method.Name));
                        }).ToArray();

                if (handler.ExecuteInTransaction)
                {
                    var db = serviceProvider.GetService<OVKDb>();

                    DataConnectionTransaction tx = null;

                    try
                    {
                        tx = await db.BeginTransactionAsync();

                        var result = await ExecuteHandlerMethod(handler.Method, handlerInstance, parameters);

                        if (!result.Success)
                        {
                            tx.Rollback();

                            return result;
                        }

                        await tx.CommitAsync();

                        return result;
                    }
                    catch (Exception)
                    {
                        await tx?.RollbackAsync();

                        throw;
                    }
                }

                return await ExecuteHandlerMethod(handler.Method, handlerInstance, parameters);
            }
            catch (Exception ex)
            {
                var log = serviceProvider.GetService<MainLogger>();
                log.LogError(ex);

                string message;

#pragma warning disable 162
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (Global.AppConfig.IsDebug)
                {
                    message = ex.ToString();
                }
                else
                {
                    message = "Грешка при обработката на заявка от тип " + request.MessageType;
                }
#pragma warning restore 162

                return GatewayResult.FromErrorMessage(message);
            }
        }


        public static HandlerCollection ScanForHandlers(params Assembly[] assemblies)
        {
            var methods = assemblies.SelectMany(assembly => assembly.GetTypes())
                                    .SelectMany(type =>
                                                    type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static |
                                                                    BindingFlags.NonPublic))
                                    .Where(info => info.GetCustomAttribute<BindRequestAttribute>() != null);

            var handlerMap = new Dictionary<Type, MethodInfo>();

            var handlers = new List<HandlerCollection.GatewayHandlerModel>();

            foreach (var methodInfo in methods)
            {
                var requestType = methodInfo.GetCustomAttribute<BindRequestAttribute>().RequestType;
                var responseType = methodInfo.GetCustomAttribute<BindRequestAttribute>().ResponseType;

                if (!methodInfo.IsPublic)
                {
                    throw new NotSupportedException(
                        string.Format(
                            "Handler binding error. The method {0} {1}.{2} is not Public.",

                            // ReSharper disable once PossibleNullReferenceException
                            requestType.Name,
                            methodInfo.DeclaringType.Name,
                            methodInfo.Name));
                }

                if (methodInfo.IsStatic)
                {
                    throw new NotSupportedException(
                        string.Format(
                            "Handler binding error. The method {0} {1}.{2} is Static.",

                            // ReSharper disable once PossibleNullReferenceException
                            requestType.Name,
                            methodInfo.DeclaringType.Name,
                            methodInfo.Name));
                }

                foreach (var parameterInfo in methodInfo.GetParameters())
                {
                    if (parameterInfo.ParameterType != requestType && parameterInfo.ParameterType != typeof(GatewayMessage))
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        throw new NotSupportedException(
                            string.Format(
                                "Parameters don't match for handler method {0}.{1}",
                                methodInfo.DeclaringType.Name,
                                methodInfo.Name));
                    }
                }

                if (handlerMap.ContainsKey(requestType))
                {
                    var existingMethodInfo = handlerMap[requestType];

                    throw new NotSupportedException(
                        "Handler binding conflict. 2 request types are bound to the same handler method." +

                        // ReSharper disable once PossibleNullReferenceException
                        string.Format("{0} => {1}.{2}", requestType.Name, existingMethodInfo.DeclaringType.Name, existingMethodInfo.Name) +

                        // ReSharper disable once PossibleNullReferenceException
                        string.Format("{0} => {1}.{2}", requestType.Name, methodInfo.DeclaringType.Name, methodInfo.Name));
                }

                var returnType = methodInfo.ReturnType;

                if (typeof(Task).IsAssignableFrom(returnType))
                {
                    if (returnType != typeof(Task) && returnType.GetGenericTypeDefinition() != typeof(Task<>))
                    {
                        throw new NotSupportedException(
                            "Invalid method return type. If the method is async only Task and Task<T> return types allowed." +
                            $"{requestType.Name} => {methodInfo.ReturnType.Name} {methodInfo.DeclaringType.Name}.{methodInfo.Name}");
                    }
                }

                handlerMap.Add(requestType, methodInfo);

                var handlerTypeAttributes = methodInfo.DeclaringType.GetCustomAttributes().ToList();
                var handlerMethodAttributes = methodInfo.GetCustomAttributes().ToList();

                var allAttributes = handlerTypeAttributes.Concat(handlerMethodAttributes).ToList();

                handlers.Add(new HandlerCollection.GatewayHandlerModel
                {
                    RequestType = requestType,
                    ResponseType = responseType,
                    Method = methodInfo,
                    HandlerType = methodInfo.DeclaringType,
                    ExecuteInTransaction = allAttributes.OfType<InTransactionAttribute>().Any(),
                    RequireAuthentication = allAttributes.OfType<AuthenticateHandlerAttribute>().Any(),
                    RpcGeneratedTypes = allAttributes.OfType<RpcGenerateAttribute>().Select(x => x.Type).ToList()
                });
            }

            return new HandlerCollection(handlers);
        }

        private static async Task<GatewayResult> ExecuteHandlerMethod(MethodInfo methodInfo, object handlerInstance, object[] parameters)
        {
            if (typeof(Task).IsAssignableFrom(methodInfo.ReturnType))
            {
                if (methodInfo.ReturnType == typeof(Task))
                {
                    await ((Task)methodInfo.Invoke(handlerInstance, parameters));

                    return GatewayResult.SuccessfulResult();
                }

                var task = (Task)methodInfo.Invoke(handlerInstance, parameters);

                await task;

                object returnValue = ((dynamic)task).Result;

                if (returnValue is GatewayResult result)
                {
                    return result;
                }

                return GatewayResult.SuccessfulResult(returnValue);
            }
            else
            {
                if (methodInfo.ReturnType == typeof(void))
                {
                    methodInfo.Invoke(handlerInstance, parameters);

                    return GatewayResult.SuccessfulResult();
                }

                var returnValue = methodInfo.Invoke(handlerInstance, parameters);

                if (returnValue is GatewayResult result)
                {
                    return result;
                }

                return GatewayResult.SuccessfulResult(returnValue);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class InTransactionAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class BindRequestAttribute : Attribute
    {
        public BindRequestAttribute(Type requestType, Type responseType = null)
        {
            this.RequestType = requestType;
            this.ResponseType = responseType;
        }

        public Type RequestType { get; }

        public Type ResponseType { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class RpcCanBeNullAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class RpcGenerateAttribute : Attribute
    {
        public Type Type { get; }

        public RpcGenerateAttribute(Type type)
        {
            this.Type = type;
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AuthenticateHandlerAttribute : Attribute
    {
    }

    public class HandlerCollection
    {
        public HandlerCollection(IReadOnlyCollection<GatewayHandlerModel> handlers)
        {
            this.Handlers = handlers;
            this.HandlersByMessageType = handlers.ToDictionary(model => model.RequestType.Name, model => model);
        }

        private IReadOnlyCollection<GatewayHandlerModel> Handlers { get; }

        private IDictionary<string, GatewayHandlerModel> HandlersByMessageType { get; }

        public List<GatewayHandlerModel> GetAllHandlers()
        {
            return this.Handlers.ToList();
        }

        public GatewayHandlerModel GetHandler(string messageType)
        {
            if (!this.HandlersByMessageType.ContainsKey(messageType))
            {
                return null;
            }

            return this.HandlersByMessageType[messageType];
        }

        public class GatewayHandlerModel
        {
            public bool ExecuteInTransaction { get; set; }

            public Type HandlerType { get; set; }

            public MethodInfo Method { get; set; }

            public Type RequestType { get; set; }

            public bool RequireAuthentication { get; set; }

            public Type ResponseType { get; set; }

            public List<Type> RpcGeneratedTypes { get; set; }
        }
    }

    public class GatewayResult
    {
        public GatewayResult()
        {
            this.Details = new List<GatewayResultDetail>();
        }

        public List<GatewayResultDetail> Details { get; set; }

        public string JSonValue { get; set; }

        public bool Success => !this.Details.Any();

        public static GatewayResult FromErrorMessage(string error)
        {
            var result = new GatewayResult();

            result.AddError(error);

            return result;
        }

        public static GatewayResult FromErrorMessages(string[] errors)
        {
            var result = new GatewayResult();

            foreach (string error in errors)
            {
                result.AddError(error);
            }

            return result;
        }


        public static GatewayResult SuccessfulResult<T>(T obj)
        {
            if (typeof(T) == typeof(GatewayResult))
            {
                return obj as GatewayResult;
            }

            return new GatewayResult
            {
                JSonValue = JsonConvert.SerializeObject(obj, GatewayProtocol.SerializerSettings)
            };
        }

        public static GatewayResult SuccessfulResult()
        {
            return new GatewayResult();
        }

        public void AddError(string message)
        {
            this.Details.Add(new GatewayResultDetail(GatewayResultDetail.ErrorType.Error, message));
        }
    }

    public class GatewayResultDetail
    {
        public GatewayResultDetail(ErrorType type, string message)
        {
            this.Type = type;
            this.Message = message;
        }

        public GatewayResultDetail()
        {
        }

        public enum ErrorType
        {
            None = 0,
            Warning = 1,
            Error = 2
        }

        public string Message { get; set; }

        public ErrorType Type { get; set; }
    }

    /// <summary>
    /// Обвиващо съобщение за изпращане на данни към Gateway сервиза
    /// </summary>
    public class GatewayMessage
    {
        /// <summary>
        /// Данните на съобщението като Json
        /// </summary>
        public string MessageJson { get; set; }

        /// <summary>
        /// Типа на съобщението
        /// </summary>
        public string MessageType { get; set; }
    }
}