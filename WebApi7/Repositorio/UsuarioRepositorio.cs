using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi7.Datos;
using WebApi7.Models;
using WebApi7.Models.DTO;
using WebApi7.Repositorio.IRepositorio;

namespace WebApi7.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _db;
        private readonly string secretKey;
        public UsuarioRepositorio(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }
        public bool IsUsuarioUnico(string userName)
        {
            var usuario = _db.Usuario.FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());

            if (usuario == null)
            {
                return true;
            }

            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var usuario = await _db.Usuario.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower()
                                                                  && u.Password == loginRequestDto.Password);

            if (usuario == null)
            {
                return new LoginResponseDto()
                {
                    Token = "",
                    Usuario = null
                };
            }

            //Generar JWT

            var tokenHandler = new  JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            usuario.Password = "none";
            LoginResponseDto loginResponseDto = new()
            {
                Token = tokenHandler.WriteToken(token),
                Usuario = usuario,

            };

            return loginResponseDto;

        }

        public async Task<Usuario> Registrar(RegistroRequestDto registroRequestDto)
        {
            Usuario usuario = new()
            {
                UserName = registroRequestDto.UserName,
                Password = registroRequestDto.Password,
                Nombres = registroRequestDto.Nombres,
                Role = registroRequestDto.Role,
            };

            await _db.Usuario.AddAsync(usuario);
            await _db.SaveChangesAsync();

            usuario.Password = ""; //formatear password

            return usuario;
        }
    }
}
