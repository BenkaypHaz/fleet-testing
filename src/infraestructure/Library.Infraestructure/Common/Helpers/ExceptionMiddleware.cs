using Library.Infraestructure.Common.ResponseHandler;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Library.Infraestructure.Common.Helpers
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment environment, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _environment = environment;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await TrySendToSentryWithEmailFallback(context, ex);

                var statusCode = 500;
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                var errorResponse = CreateErrorResponse(ex, statusCode);

                var jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                await context.Response.WriteAsync(jsonResponse);
            }
        }

        private async Task TrySendToSentryWithEmailFallback(HttpContext context, Exception exception)
        {
            try
            {
                SentrySdk.CaptureException(exception);
            }
            catch (Exception sentryEx)
            {
                _logger.LogWarning(sentryEx, "Fallo al enviar excepción a Sentry, intentando enviar email de respaldo");

                await SendFallbackEmail(context, exception, sentryEx);
            }
        }

        private async Task SendFallbackEmail(HttpContext context, Exception originalException, Exception sentryException)
        {
            try
            {
                var requestPath = context?.Request?.Path.Value ?? "N/A";
                var requestMethod = context?.Request?.Method ?? "N/A";
                var userAgent = context?.Request?.Headers["User-Agent"].ToString() ?? "N/A";
                var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss UTC");

                var emailTemplate = $@"
                <html>
                <body>
                    <h2>🚨 ALERTA: Fallo en Sistema de Monitoreo</h2>
                    <p><strong>Fecha y Hora:</strong> {timestamp}</p>
                    
                    <h3>📊 Problema con Sentry</h3>
                    <p><strong>Motivo:</strong> {sentryException.Message}</p>
                    <p><strong>Tipo de Error Sentry:</strong> {sentryException.GetType().Name}</p>
                    
                    <h3>🔥 Excepción Original del Sistema</h3>
                    <p><strong>Mensaje:</strong> {originalException.Message}</p>
                    <p><strong>Tipo:</strong> {originalException.GetType().Name}</p>
                    <p><strong>Endpoint:</strong> {requestMethod} {requestPath}</p>
                    <p><strong>User Agent:</strong> {userAgent}</p>
                    
                    <h3>📋 Stack Trace</h3>
                    <pre style='background-color: #f5f5f5; padding: 10px; border-radius: 5px; font-size: 12px;'>
{originalException.StackTrace}
                    </pre>

                    <h3>🔧 Acciones Recomendadas</h3>
                    <ul>
                        <li>Verificar el estado de Sentry</li>
                        <li>Revisar la configuración de Sentry en el servidor</li>
                        <li>Validar conectividad de red</li>
                        <li>Investigar la excepción original del sistema</li>
                    </ul>

                    <p><em>Este email se generó automáticamente cuando Sentry no pudo procesar una excepción.</em></p>
                </body>
                </html>";

                await BaseHelper.SendEmail(
                    NameRecipient: "Equipo IT",
                    emailRecipient: "it@coreexpress.com",
                    template: emailTemplate,
                    subject: "🚨 URGENTE: Fallo en Monitoreo de Excepciones - Sentry Inaccesible"
                );

                _logger.LogInformation("Email de respaldo enviado exitosamente por fallo de Sentry");
            }
            catch (Exception emailEx)
            {
                _logger.LogError(emailEx, "Error crítico: Falló tanto Sentry como el envío de email de respaldo");

            }
        }


        private GenericResponseHandler<object> CreateErrorResponse(Exception ex, int statusCode)
        {
            return new GenericResponseHandler<object>(
                statusCode: statusCode,
                data: null,
                dataRecords: 0,
                message: null,
                exceptionMessage: ex.Message
            );
        }
    }
}