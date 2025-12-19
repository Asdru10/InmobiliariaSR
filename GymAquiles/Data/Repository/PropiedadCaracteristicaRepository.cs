using ProyectoInmobilaria.Data.Repository.Interfaces;
using ProyectoInmobilaria.Models;

namespace ProyectoInmobilaria.Data.Repository
{
    public class PropiedadCaracteristicaRepository : Repository<PropiedadCaracteristica>, IPropiedadCaracteristicaRepository
    {

        private ApplicationDbContext _db;

        public PropiedadCaracteristicaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PropiedadCaracteristica propiedadCaracteristica)
        {
            _db.PropiedadCaracteristicas.Update(propiedadCaracteristica);
        }
    }
}

