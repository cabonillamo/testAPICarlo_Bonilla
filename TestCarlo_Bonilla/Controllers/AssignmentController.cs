using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestCarlo_Bonilla.Models;
using Microsoft.EntityFrameworkCore;

namespace TestCarlo_Bonilla.Controllers
{
 
    [ApiController]
    [Route("[controller]")]
    public class AssignmentController : Controller
    {
        // Declara el contexto de la base de datos como un campo privado
        private readonly AsodbContext _context;

        // Constructor que inyecta el contexto de la base de datos
        public AssignmentController(AsodbContext context)
        {
            _context = context;
        }

     
        [HttpGet]
        public async Task<IActionResult> GetAsignaciones()
        {
            try
            {
                // Usamos AsNoTracking para mejorar el rendimiento, ya que no modificaremos los datos.
                var asignaciones = await _context.Asignaciones
                    .FromSqlRaw("EXEC AsignarMontos") // Ejecuta el procedimiento almacenado
                    .AsNoTracking() // Optimización para consultas de solo lectura
                    .ToListAsync();  // Ejecutamos la consulta de manera asíncrona

                return Ok(asignaciones); 
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Se produjo un error al obtener las asignaciones: {ex.Message}");
            }
        }
    }
}
