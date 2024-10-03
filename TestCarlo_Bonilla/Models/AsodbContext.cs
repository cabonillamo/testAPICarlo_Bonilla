using Microsoft.EntityFrameworkCore;

namespace TestCarlo_Bonilla.Models
{
    
    public class AsodbContext : DbContext
    {
        // Constructor que recibe las opciones del contexto y las pasa a la clase base
        public AsodbContext(DbContextOptions<AsodbContext> options) : base(options) { }

        // DbSet que representa la colección de asignaciones en la base de datos
        public DbSet<Assignment> Asignaciones { get; set; }

    }
}
