namespace WebApi7.Models.DTO
{
    public class LoginResponseDto
    {
        public UsuarioIdentityDto Usuario { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
