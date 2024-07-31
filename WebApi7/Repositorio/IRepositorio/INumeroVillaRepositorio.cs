using WebApi7.Models;

namespace WebApi7.Repositorio.IRepositorio
{
    public interface INumeroVillaRepositorio: IRepositorio<NumeroVilla>
    {
        Task<NumeroVilla> Actualizar(NumeroVilla numeroVilla);
    }
}
