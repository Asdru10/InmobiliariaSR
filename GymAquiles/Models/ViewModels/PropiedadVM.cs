using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProyectoInmobilaria.Models.ViewModels
{
    public class PropiedadVM
    {
        [ValidateNever]
        public Propiedad Propiedad { get; set; }

        [ValidateNever]
        public IFormFile Foto { get; set; }

        [ValidateNever]
        public string UrlFoto { get; set; }

        [ValidateNever]
        public List<IFormFile> Fotografias { get; set; }

        [ValidateNever]
        public List<string> URLs { get; set; }

        [ValidateNever]
        public Ubicacion Ubicacion { get; set; }

    [Display(Name = "Coordenadas")]
    [ValidateNever]
    public string Coordenadas { get; set; }


        [ValidateNever]
        public PropiedadesIndexVM propiedadesIndexVM { get; set; }

        [ValidateNever]
        public List<Caracteristicas> TodasLasCaracteristicas { get; set; } = new();

        [ValidateNever]
        // IDs de características seleccionadas
        public List<int> CaracteristicasSeleccionadas { get; set; } = new();

        [ValidateNever]
        public bool EsFavorita { get; set; }

        [ValidateNever]
        public string NumeroTelefono { get; set; }

        [ValidateNever]
        public string CorreoElectronico { get; set; }
    }
}
