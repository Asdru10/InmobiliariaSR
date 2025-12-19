using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProyectoInmobilaria.Models
{
    public class Ubicacion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Pais { get; set; }

        [Required]
        [StringLength(40)]
        public string Provincia { get; set; }

        [Required]
        [StringLength(40)]
        public string Ciudad { get; set; }

        [Required]
        [StringLength(100)]
        public string Direccion { get; set; }

        [Display(Name = "Propiedad")]
        public int PropiedadId { get; set; }

        [ForeignKey("PropiedadId")]
        public virtual Propiedad Propiedad { get; set; }
        [StringLength(50)]
        public string Latitud { get; set; }

        [StringLength(50)]
        public string Longitud { get; set; }
    }
}
