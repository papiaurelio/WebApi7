using WebApi7.Datos;
using WebApi7.Models;
using WebApi7.Repositorio.IRepositorio;

namespace WebApi7.Repositorio
{
    public class NumeroVillaRepositorio : Repositorio<NumeroVilla>, INumeroVillaRepositorio
    {
        private readonly ApplicationDbContext _context;

        public NumeroVillaRepositorio(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<NumeroVilla> Actualizar(NumeroVilla entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _context.NumeroVilla.Update(entidad);
            await _context.SaveChangesAsync();

            return entidad;
        }
    }
}
