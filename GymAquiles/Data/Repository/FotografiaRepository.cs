using ProyectoInmobilaria.Data.Repository.Interfaces;
using ProyectoInmobilaria.Models;

namespace ProyectoInmobilaria.Data.Repository
{
    public class FotografiaRepository : Repository<Fotografia>, IFotografiaRepository
    {
        private ApplicationDbContext _db;

        public FotografiaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
