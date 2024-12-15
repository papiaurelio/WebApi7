using Villa_Web.Models;

namespace Villa_Web.Services.IServices
{
    public interface INumeroVillaService
    {
        Task<T> ObtenerTodos<T>();
        Task<T> Obtener<T>(int id);
        Task<T> Crear<T>(CrearNumeroVillaDto villaDto);
        Task<T> Actualizar<T>(ActualizarNumeroVillaDto villaDto);
        Task<T> Remover<T>(int id);

    }
}
