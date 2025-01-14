using Villa_Web.Models;

namespace Villa_Web.Services.IServices
{
    public interface IVillaService
    {
        Task<T> ObtenerTodos<T>(string token);
        Task<T> ObtenerTodosPaginado<T>(string token, int pageNumber = 1, int pageSize = 4);
        Task<T> Obtener<T>(int id, string token);
        Task<T> Crear<T>(CrearVillaDto villaDto, string token);
        Task<T> Actualizar<T>(ActualizarVillaDto villaDto, string token);
        Task<T> Remover<T>(int id, string token);

    }
}
