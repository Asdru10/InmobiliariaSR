using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.CodeAnalysis;
using System.Numerics;

namespace ProyectoInmobilaria.Models
{
    public class PropiedadCaracteristica
    {
        [Display(Name = "Caracteristicas")]
        public int CaracteristicasId { get; set; }


        [Required]
        [ForeignKey("CaracteristicasId")]
        public Caracteristicas Caracteristicas { get; set; }


        [Display(Name = "Propiedad")]
        public int PropiedadId { get; set; }


        [Required]
        [ForeignKey("PropiedadId")]
        public Propiedad Propiedad { get; set; }
    }
}
