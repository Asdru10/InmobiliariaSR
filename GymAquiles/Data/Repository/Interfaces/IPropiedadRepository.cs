using ProyectoInmobilaria.Models;

namespace ProyectoInmobilaria.Data.Repository.Interfaces
{
    public interface IPropiedadRepository : IRepository<Propiedad>
    {
        void Update(Propiedad propiedad);
    }
}
