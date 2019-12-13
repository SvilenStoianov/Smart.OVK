using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Smart.Web.Infrastructure;

namespace Smart.Web.Infrastructure
{
    public class AuthMiddleware
    {
        private const string AuthorizationHeaderName = "Authorization";

        private const string SchemeName = "Basic";

        private readonly RequestDelegate next;

        public AuthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        private static string GetToken(HttpContext context, MainLogger logger)
        {
            try
            {
                string headerValue = context.Request.Headers[AuthorizationHeaderName];

                if (string.IsNullOrWhiteSpace(headerValue))
                {
                    // Invalid authorization header
                    return null;
                }

                var parts = headerValue.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                {
                    // Invalid authorization header
                    return null;
                }

                string scheme = parts[0];

                if (scheme != SchemeName)
                {
                    // Unsupported scheme.
                    return null;
                }

                string token = parts[1];

                return token;
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return null;
            }
        }

        public async Task InvokeAsync(HttpContext context, OVKDb db, MainLogger logger)
        {
            var requestSession = new RequestSession
            {
                IsAuthenticated = false,
            };

            context.SetRequestSession(requestSession);

            string token = GetToken(context, logger);

            if (token == null)
            {
                // Invalid authorization header
                await this.next(context);
                return;
            }

            byte[] debase64 = Convert.FromBase64String(token);
            string[] temp = Encoding.UTF8.GetString(debase64).Split(':');
            if(temp.Length != 2)
            {
                // Invalid authorization header
                await this.next(context);
                return;
            }

            string apiKey = temp[1];
            var poco = db.Poco.s_settings
                .Where(p => p.setting_id == 1 && p.setting_value == apiKey)
                .FirstOrDefault();
            if (poco == null)
            {
                // No such session.
                await this.next(context);
                return;
            }

            requestSession.IsAuthenticated = true;
            requestSession.ApiKey = poco.setting_value;

            await this.next(context);
        }
    }

    public static class AuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }

    public static class RequestExtensions
    {
        private const string RequestClientKey = "__request_client__";

        public static RequestSession GetRequestSession(this HttpContext ctx)
        {
            return (RequestSession)ctx.Items[RequestClientKey];
        }

        public static void SetRequestSession(this HttpContext ctx, RequestSession requestSession)
        {
            ctx.Items[RequestClientKey] = requestSession;
        }
    }

    public class RequestSession
    {
        public string ApiKey { get; set; }

        public bool IsAuthenticated { get; set; }
    }

}