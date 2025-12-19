using ProyectoInmobilaria.Data.Repository.Interfaces;
using ProyectoInmobilaria.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ProyectoInmobilaria.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            //convertimos el dbseten un querytable para poder filtrar por expresiones 
            IQueryable<T> query = dbSet;

            //Se debe hacer un if para verificar que si se esta pidiendo hacer eager loading
            if (includeProperties != null)
            {
                //y se realiza un foreach para que vaya  cargando cada una de las propiedades especificadas
                //el string con las propiedades a cargar se deben separar por comas y remueve espacios vacios
                foreach (var prop in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    //El query  incluye las propiedades que se desean cargar 
                    query = query.Include(prop);
                }
            }

            //Toma toda la  informacion filtrada por la exprecion y solo retorna el primero
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var prop in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }

            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
           dbSet.RemoveRange(entities);
        }
        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

    }
}
