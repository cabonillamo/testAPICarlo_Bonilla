using Microsoft.EntityFrameworkCore;
using TestCarlo_Bonilla.Middlewares;
using TestCarlo_Bonilla.Models; 

// Crea un generador de aplicaciones web
var builder = WebApplication.CreateBuilder(args);

// Agrega servicios al contenedor de la aplicación
builder.Services.AddControllers(); // Registra los controladores para manejar las solicitudes HTTP

// Configura la cadena de conexión y registra el contexto de la base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("La cadena de conexión no se encontró en el archivo de configuración."); // Lanza una excepción si no se encuentra la cadena de conexión

// Agrega el contexto de la base de datos al contenedor, utilizando SQL Server como proveedor
builder.Services.AddDbContext<AsodbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.CommandTimeout(30))); // tiempo de espera de 30 segundos

// Configura Swagger para la documentación de la API
builder.Services.AddEndpointsApiExplorer(); // Permite explorar los endpoints
builder.Services.AddSwaggerGen(); // Genera la documentación Swagger

// Construye la aplicación
var app = builder.Build();

// Configura el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment()) // Comprueba si la aplicación se está ejecutando en un entorno de desarrollo
{
    app.UseSwagger(); // Activa Swagger en el entorno de desarrollo
    app.UseSwaggerUI(); // Activa la interfaz de usuario de Swagger
}
app.UseMiddleware<SignOutMiddleware>();
app.UseHttpsRedirection(); // Redirige automáticamente las solicitudes HTTP a HTTPS para mayor seguridad
app.UseAuthorization(); // Añade el middleware de autorización para manejar las políticas de acceso
app.MapControllers(); // Mapea los controladores de la aplicación a las rutas

app.Run(); // Inicia la aplicación y comienza a escuchar las solicitudes
