using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoInmobilaria.Models.ViewModels
{
    public class PropiedadesIndexVM
    {
        public List<PropiedadVM> PropiedadesVm { get; set; } = new();
        public IEnumerable<Propiedad> Propiedades { get; set; }
        public string SelectedCurrency { get; set; } = "CRC";
        public IEnumerable<string> AvailableCurrencies { get; set; }
        public Dictionary<string, string> CurrencySymbols { get; set; }

        // Filtros seleccionados por el usuario
        public decimal? PrecioMinimo { get; set; }
        public decimal? PrecioMaximo { get; set; }

        public string EstadoSeleccionado { get; set; }
        public string TipoSeleccionado { get; set; }

        public List<int> CaracteristicasSeleccionadas { get; set; } = new();

        public List<Caracteristicas> TodasLasCaracteristicas { get; set; } = new();

        // Datos para llenar los filtros (dropdowns, checkboxes)
        public List<SelectListItem> EstadosDisponibles { get; set; } = new();
        public List<SelectListItem> TiposDisponibles { get; set; } = new();
        public List<SelectListItem> CaracteristicasDisponibles { get; set; } = new();

        public List<PropiedadVM> PropiedadesDestacadas { get; set; } = new();
    }
}
