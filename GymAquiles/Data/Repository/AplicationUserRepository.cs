using ProyectoInmobilaria.Data.Repository.Interfaces;
using ProyectoInmobilaria.Models;

namespace ProyectoInmobilaria.Data.Repository
{
    public class AplicationUserRepository : Repository<AplicationUser>, Interfaces.IAplicationUserRepository
    {

        private ApplicationDbContext _db;

        public AplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(AplicationUser usuario)
        {
            _db.ApplicationUsers.Update(usuario);
        }
    }
}

