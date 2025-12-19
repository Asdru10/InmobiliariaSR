using ProyectoInmobilaria.Data.Repository.Interfaces;

namespace ProyectoInmobilaria.Data.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IUbicacionRepository Ubicacion { get; }
        IContactoRepository Contacto { get; }
        ICaracteristicasRepository Caracteristicas { get; }
        IPropiedadRepository Propiedad { get; }
        IPropiedadCaracteristicaRepository PropiedadCaracteristica { get; }

        IAplicationUserRepository AplicationUser { get; }

        IFotografiaRepository Fotografia { get; }

        IFavoritoRepository Favorito { get; }

        void Save();
    }
}
