using ProyectoInmobilaria.Data.Repository.Interfaces;
using ProyectoInmobilaria.Models;

namespace ProyectoInmobilaria.Data.Repository
{
    public class PropiedadRepository : Repository<Propiedad>, IPropiedadRepository
    {

        private ApplicationDbContext _db;

        public PropiedadRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}

