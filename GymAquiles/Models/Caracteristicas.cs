using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProyectoInmobilaria.Models
{
    public class Caracteristicas
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Caracteristica { get; set; }
    }
}
