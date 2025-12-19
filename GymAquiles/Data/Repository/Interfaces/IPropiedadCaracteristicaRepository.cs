using ProyectoInmobilaria.Models;

namespace ProyectoInmobilaria.Data.Repository.Interfaces
{
    public interface IPropiedadCaracteristicaRepository : IRepository<PropiedadCaracteristica>
    {
        void Update(PropiedadCaracteristica propiedadCaracteristica);
    }
}
