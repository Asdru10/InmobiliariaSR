using ProyectoInmobilaria.Data.Repository.Interfaces;
using ProyectoInmobilaria.Models;

namespace ProyectoInmobilaria.Data.Repository
{
    public class ContactoRepository : Repository<Contacto>, IContactoRepository
    {

        private ApplicationDbContext _db;

        public ContactoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Contacto contacto)
        {
            _db.Contactos.Update(contacto);
        }
    }
}

