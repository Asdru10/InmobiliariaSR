
using ProyectoInmobilaria.Models;

namespace ProyectoInmobilaria.Data.Repository.Interfaces
{
    public interface IFavoritoRepository : IRepository<Favorito>
    {
        IEnumerable<Favorito> GetAll(Func<object, bool> value);
        void Update(Favorito favorito);
    }
}
