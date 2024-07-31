using WebApi7.Models;

namespace WebApi7.Repositorio.IRepositorio
{
    public interface IVillaRepositorio: IRepositorio<Villa>
    {
        Task<Villa> Actualizar(Villa villa);
    }
}
