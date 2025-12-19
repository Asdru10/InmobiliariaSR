using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoInmobilaria.Models
{
    public class Favorito
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "User")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AplicationUser User { get; set; }

        public int PropiedadId { get; set; }

        public Propiedad Propiedad { get; set; }
    }

}
