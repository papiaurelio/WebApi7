using WebApi7.Models;
using WebApi7.Models.DTO;

namespace WebApi7.Repositorio.IRepositorio
{
    public interface IUsuarioRepositorio
    {
        bool IsUsuarioUnico(string usuario);

        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        Task<UsuarioIdentityDto> Registrar(RegistroRequestDto registroRequestDto);
    }
}
