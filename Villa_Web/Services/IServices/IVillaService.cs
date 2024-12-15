using Villa_Web.Models;

namespace Villa_Web.Services.IServices
{
    public interface IVillaService
    {
        Task<T> ObtenerTodos<T>();
        Task<T> Obtener<T>(int id);
        Task<T> Crear<T>(CrearVillaDto villaDto);
        Task<T> Actualizar<T>(ActualizarVillaDto villaDto);
        Task<T> Remover<T>(int id);

    }
}
