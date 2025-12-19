using ProyectoInmobilaria.Data.Repository.Interfaces;
using ProyectoInmobilaria.Models;

namespace ProyectoInmobilaria.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            AplicationUser = new AplicationUserRepository(_db);
            Ubicacion = new UbicacionRepository(_db);
            Contacto = new ContactoRepository(_db);
            Caracteristicas = new CaracteristicasRepository(_db);
            Propiedad = new PropiedadRepository(_db);
            PropiedadCaracteristica = new PropiedadCaracteristicaRepository(_db);
            Fotografia = new FotografiaRepository(_db);
            Favorito = new FavoritoRepository(_db);
        }
        public IUbicacionRepository Ubicacion { get; private set; }

        public IContactoRepository Contacto { get; private set; }

        public ICaracteristicasRepository Caracteristicas { get; private set; }
        public IPropiedadRepository Propiedad { get; private set; }

        public IPropiedadCaracteristicaRepository PropiedadCaracteristica { get; private set; }

        public Interfaces.IAplicationUserRepository AplicationUser { get; private set; }

        public IFotografiaRepository Fotografia { get; private set; }

        public IFavoritoRepository Favorito { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
