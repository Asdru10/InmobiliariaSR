using ProyectoInmobilaria.Data.Repository.Interfaces;
using ProyectoInmobilaria.Models;

namespace ProyectoInmobilaria.Data.Repository
{
    public class UbicacionRepository : Repository<Ubicacion>, IUbicacionRepository
    {

        private ApplicationDbContext _db;

        public UbicacionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Ubicacion ubicacion)
        {
            _db.Ubicaciones.Update(ubicacion);
        }
    }
}

