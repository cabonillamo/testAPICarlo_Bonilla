using System.Net; 

namespace TestCarlo_Bonilla.Middlewares
{
    
    public class SignOutMiddleware
    {
        private readonly RequestDelegate _next; 
        private readonly ILogger<SignOutMiddleware> _logger; // Logger para registrar errores

        // Constructor que recibe el siguiente middleware y un logger
        public SignOutMiddleware(RequestDelegate next, ILogger<SignOutMiddleware> logger)
        {
            _next = next; 
            _logger = logger; 
        }

        // Método que se invoca para procesar la solicitud
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Llama al siguiente middleware en la cadena de procesamiento
                await _next(context);
            }
            catch (Exception ex) 
            {
                // Registra el error utilizando el logger
                _logger.LogError(ex, "Ocurrió un error: {Message}", ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // Escribe un mensaje en la respuesta para el cliente
                await context.Response.WriteAsync("Ocurrió un error inesperado. Por favor, inténtelo de nuevo más tarde.");
            }
        }
    }
}
