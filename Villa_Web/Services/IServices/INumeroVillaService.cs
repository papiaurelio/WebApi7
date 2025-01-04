using Villa_Web.Models;

namespace Villa_Web.Services.IServices
{
    public interface INumeroVillaService
    {
        Task<T> ObtenerTodos<T>(string token);
        Task<T> Obtener<T>(int id, string token);
        Task<T> Crear<T>(CrearNumeroVillaDto villaDto, string token);
        Task<T> Actualizar<T>(ActualizarNumeroVillaDto villaDto, string token);
        Task<T> Remover<T>(int id, string token);

    }
}
