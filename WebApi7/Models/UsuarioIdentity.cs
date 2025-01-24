using Microsoft.AspNetCore.Identity;

namespace WebApi7.Models
{
    public class UsuarioIdentity : IdentityUser
    {
        public string Nombres { get; set; }
        public string Cedula { get; set; }

    }
}
