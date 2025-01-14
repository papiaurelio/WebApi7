using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using WebApi7.Datos;
using WebApi7.Models.Especificaciones;
using WebApi7.Repositorio.IRepositorio;

namespace WebApi7.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {

        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;

        public Repositorio(ApplicationDbContext context)
        {
            _context = context;
            
            //setea como una entidad de la base de datos.
            this.dbSet = _context.Set<T>();
        }
        public async Task Crear(T entidad)
        {
            await dbSet.AddAsync(entidad);
            await Guardar();
        }

        public async Task Guardar()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true, string? incluirPropiedades = null)
        {
            IQueryable<T> query = dbSet;
            if(!tracked) 
            {
                query = query.AsNoTracking();
            }

            if (filtro != null)  
            {
                query = query.Where(filtro);
            }

            if(incluirPropiedades != null) //Villa.otroModelo
            { 
                foreach(var incluirProp  in incluirPropiedades.Split(new char[] {'.'}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, string? incluirPropiedades = null)
        {
            IQueryable<T> query = dbSet;

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            if (incluirPropiedades != null) //Villa.otroModelo
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);
                }
            }

            return await query.ToListAsync();
        }

        public PagedList<T> ObtenerTodosPaginado(Parametros parametros, Expression<Func<T, bool>> filtro = null, string? incluirPropiedades = null)
        {
            IQueryable<T> query = dbSet;

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            if (incluirPropiedades != null) //Villa.otroModelo
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);
                }
            }

            return PagedList<T>.ToPagedList(query, parametros.PageNumber, parametros.PageSize);
        }

        public async Task Remover(T entidad)
        {
            dbSet.Remove(entidad);
            await Guardar();
        }
    }
}
