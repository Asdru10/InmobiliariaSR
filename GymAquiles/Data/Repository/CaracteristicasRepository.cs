using ProyectoInmobilaria.Data.Repository.Interfaces;
using ProyectoInmobilaria.Models;

namespace ProyectoInmobilaria.Data.Repository
{
    public class CaracteristicasRepository : Repository<Caracteristicas>, ICaracteristicasRepository
    {

        private ApplicationDbContext _db;

        public CaracteristicasRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Caracteristicas caracteristicas)
        {
            _db.Caracteristicas.Update(caracteristicas);
        }
    }
}

