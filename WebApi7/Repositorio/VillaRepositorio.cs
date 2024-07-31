using WebApi7.Datos;
using WebApi7.Models;
using WebApi7.Repositorio.IRepositorio;

namespace WebApi7.Repositorio
{
    public class VillaRepositorio : Repositorio<Villa>, IVillaRepositorio
    {
        private readonly ApplicationDbContext _context;

        public VillaRepositorio(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Villa> Actualizar(Villa entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _context.Update(entidad);
            await _context.SaveChangesAsync();

            return entidad;
        }
    }
}
