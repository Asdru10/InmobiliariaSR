using ProyectoInmobilaria.Models;

namespace ProyectoInmobilaria.Data.Repository.Interfaces
{
    public interface IFotografiaRepository : IRepository<Fotografia>
    {

        void Update(Fotografia fotografia);

    }
}
