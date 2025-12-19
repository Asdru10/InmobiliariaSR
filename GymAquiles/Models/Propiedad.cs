using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProyectoInmobilaria.Models
{
    public class Propiedad
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }

        [Required]
        public int Likes { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; }

        [Required]
        public DateTime Fecha_publicacion { get; set; }

        [Required]
        [StringLength(3)]
        public string Moneda { get; set; } = "CRC";

        [Required]
        [StringLength(30)]
        public string Tipo { get; set; }

        [Display(Name = "User")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AplicationUser User { get; set; }

        public virtual ICollection<Fotografia> Fotografias { get; set; }

        public virtual ICollection<PropiedadCaracteristica> PropiedadCaracteristicas { get; set; }

        public virtual ICollection<Favorito> Favoritos { get; set; }

        public virtual Ubicacion Ubicacion { get; set; }

    }
}
