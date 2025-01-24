using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<UsuarioIdentity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UsuarioRepositorio(ApplicationDbContext db, IConfiguration configuration, UserManager<UsuarioIdentity> userManager,
                        IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        public bool IsUsuarioUnico(string userName)
        {
            var usuario = _db.UsuarioIdentity.FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());

            if (usuario == null)
            {
                return true;
            }

            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var usuario = await _db.UsuarioIdentity.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(usuario, loginRequestDto.Password);

            if (usuario == null || isValid == false)
            {
                return new LoginResponseDto()
                {
                    Token = "",
                    Usuario = null
                };
            }

            //Generar JWT
            var roles = await _userManager.GetRolesAsync(usuario);
            var tokenHandler = new  JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.UserName),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseDto loginResponseDto = new()
            {
                Token = tokenHandler.WriteToken(token),
                Usuario = _mapper.Map<UsuarioIdentityDto>(usuario)
            };

            return loginResponseDto;

        }

        public async Task<UsuarioIdentityDto> Registrar(RegistroRequestDto registroRequestDto)
        {
            UsuarioIdentity usuario = new()
            {
                UserName = registroRequestDto.UserName,
                Email = registroRequestDto.UserName,
                NormalizedEmail = registroRequestDto.UserName.ToUpper(),
                Nombres = registroRequestDto.Nombres,
            };

            try
            {
                var resultado = await _userManager.CreateAsync(usuario, registroRequestDto.Password);
                if (resultado.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("admin"));
                        await _roleManager.CreateAsync(new IdentityRole("cliente"));
                    }
                    await _userManager.AddToRoleAsync(usuario, "admin");
                    var usuarioAp = _db.UsuarioIdentity.FirstOrDefault(u => u.UserName == registroRequestDto.UserName );

                    return _mapper.Map<UsuarioIdentityDto>(usuarioAp);        
                }
            }
            catch (Exception)
            {
                throw;
            }

            return new UsuarioIdentityDto();
        }
    }
}
