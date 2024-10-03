using Microsoft.EntityFrameworkCore;
using TestCarlo_Bonilla.Middlewares;
using TestCarlo_Bonilla.Models; 

// Crea un generador de aplicaciones web
var builder = WebApplication.CreateBuilder(args);

// Agrega servicios al contenedor de la aplicaci�n
builder.Services.AddControllers(); // Registra los controladores para manejar las solicitudes HTTP

// Configura la cadena de conexi�n y registra el contexto de la base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("La cadena de conexi�n no se encontr� en el archivo de configuraci�n."); // Lanza una excepci�n si no se encuentra la cadena de conexi�n

// Agrega el contexto de la base de datos al contenedor, utilizando SQL Server como proveedor
builder.Services.AddDbContext<AsodbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.CommandTimeout(30))); // tiempo de espera de 30 segundos

// Configura Swagger para la documentaci�n de la API
builder.Services.AddEndpointsApiExplorer(); // Permite explorar los endpoints
builder.Services.AddSwaggerGen(); // Genera la documentaci�n Swagger

// Construye la aplicaci�n
var app = builder.Build();

// Configura el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment()) // Comprueba si la aplicaci�n se est� ejecutando en un entorno de desarrollo
{
    app.UseSwagger(); // Activa Swagger en el entorno de desarrollo
    app.UseSwaggerUI(); // Activa la interfaz de usuario de Swagger
}
app.UseMiddleware<SignOutMiddleware>();
app.UseHttpsRedirection(); // Redirige autom�ticamente las solicitudes HTTP a HTTPS para mayor seguridad
app.UseAuthorization(); // A�ade el middleware de autorizaci�n para manejar las pol�ticas de acceso
app.MapControllers(); // Mapea los controladores de la aplicaci�n a las rutas

app.Run(); // Inicia la aplicaci�n y comienza a escuchar las solicitudes
