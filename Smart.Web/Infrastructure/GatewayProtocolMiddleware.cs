using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Smart.Web.Infrastructure
{
    public class GatewayProtocolMiddleware
    {
        private readonly RequestDelegate next;

        public GatewayProtocolMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            if (context.Request.Path != "/api/gateway/private")
            {
                await this.next(context);
                return;
            }

            string body;

            using (var bodyStream = context.Request.Body)
            using (var streamReader = new StreamReader(bodyStream))
            {
                body = await streamReader.ReadToEndAsync();
            }

            var message = JsonConvert.DeserializeObject<GatewayMessage>(body, GatewayProtocol.SerializerSettings);

            var gatewayResult = await GatewayProtocol.ProcessGatewayMessage(
                message,
                Global.Handlers,
                serviceProvider
            );

            string responseBody = JsonConvert.SerializeObject(gatewayResult, GatewayProtocol.SerializerSettings);

            await context.Response.WriteAsync(responseBody);
        }
    }

    public static class GatewayProtocolMiddlewareExtensions
    {
        public static IApplicationBuilder UseGatewayProtocolMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GatewayProtocolMiddleware>();
        }
    }
}