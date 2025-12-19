using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoInmobilaria.Models
{
    public class AplicationUser : IdentityUser
    {

        [Required]
        public string Cedula { get; set; }


        [Required]
        public string FullName { get; set; }

        public virtual ICollection<Favorito> Favoritos { get; set; }

    }
}
