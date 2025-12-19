using ProyectoInmobilaria.Models;

namespace ProyectoInmobilaria.Data.Repository.Interfaces
{
    public interface IContactoRepository : IRepository<Contacto>
    {
        void Update(Contacto contacto);
    }
}
