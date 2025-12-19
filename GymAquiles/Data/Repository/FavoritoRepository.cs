using ProyectoInmobilaria.Data.Repository.Interfaces;
using ProyectoInmobilaria.Models;

namespace ProyectoInmobilaria.Data.Repository
{
    public class FavoritoRepository : Repository<Favorito>, IFavoritoRepository
    {

        private ApplicationDbContext _db;

        public FavoritoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Favorito> GetAll(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}

