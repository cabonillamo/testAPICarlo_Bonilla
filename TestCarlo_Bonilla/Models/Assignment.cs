using System.ComponentModel.DataAnnotations;

namespace TestCarlo_Bonilla.Models
{

    public class Assignment
    {
        [Key] // Asegúrate de importar System.ComponentModel.DataAnnotations
        public int Id { get; set; } // Agregar una clave primaria única

        // Propiedad que representa el identificador del gestor
        [Range(1, int.MaxValue, ErrorMessage = "GestorId debe ser un valor positivo.")]
        public int GestorId { get; set; } // La anotación [Range] asegura que el GestorId debe ser un número positivo

        // Propiedad que representa el monto del saldo asignado
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que 0.")]
        public decimal SaldoMonto { get; set; } // La anotación [Range] asegura que el SaldoMonto debe ser mayor que 0
    }
}
