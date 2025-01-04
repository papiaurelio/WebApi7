using Villa_Web.Models.DTO;

namespace Villa_Web.Services.IServices
{
    public interface IUsuarioService
    {
        Task<T> Login<T>(LoginRequestDto loginDto);

        Task<T> Registrar<T>(RegistroRequestDto registroDto);
    }
}
