using ProyectoInmobilaria.Models;

namespace ProyectoInmobilaria.Data.Repository.Interfaces
{
    public interface IUbicacionRepository : IRepository<Ubicacion>
    {
        void Update(Ubicacion ubicacion);
    }
}
