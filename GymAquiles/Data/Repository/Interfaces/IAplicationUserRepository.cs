using ProyectoInmobilaria.Models;

namespace ProyectoInmobilaria.Data.Repository.Interfaces
{
    public interface IAplicationUserRepository : IRepository<AplicationUser>
    {
        void Update(AplicationUser usuario);
    }
}
