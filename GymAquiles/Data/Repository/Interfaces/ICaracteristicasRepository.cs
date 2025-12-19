using ProyectoInmobilaria.Models;
using System.Reflection.PortableExecutable;

namespace ProyectoInmobilaria.Data.Repository.Interfaces
{
    public interface ICaracteristicasRepository : IRepository<Caracteristicas>
    {
        void Update(Caracteristicas caracteristicas);
    }
}
