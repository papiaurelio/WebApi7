using System.Linq.Expressions;

namespace WebApi7.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        Task Crear(T entidad);

        Task<List<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, string? incluirPropiedades = null);

        //tracked para trabajar con el objeto sin tener problemas de reutilizacion
        Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true, string? incluirPropiedades = null);

        Task Remover(T entidad);

        Task Guardar();

    }
}
