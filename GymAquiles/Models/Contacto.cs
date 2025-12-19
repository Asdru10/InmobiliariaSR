using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProyectoInmobilaria.Models
{
    public class Contacto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Emisor { get; set; }

        [Required]
        public int Receptor { get; set; }

        [Required]
        public string Mensaje { get; set; }

        [Required]
        public DateTime Fecha_mensaje { get; set; }

        [Display(Name = "User")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AplicationUser User { get; set; }
    }
}
