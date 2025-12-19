using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProyectoInmobilaria.Models.ViewModels
{
    public class FavoritoVM
    {
        [ValidateNever]
        public Favorito Favorito { get; set; }

        [ValidateNever]
        public IEnumerable<Favorito> Favoritos { get; set; }
    }
}
