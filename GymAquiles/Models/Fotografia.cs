using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoInmobilaria.Models
{
    public class Fotografia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        // Relación con la propiedad
        [Required]
        public int PropiedadId { get; set; }

        [ForeignKey("PropiedadId")]
        public Propiedad Propiedad { get; set; }
    }
}
