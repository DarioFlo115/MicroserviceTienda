using TiendaServicios.Api.CarritoCompras.RemoteModel;

namespace TiendaServicios.Api.CarritoCompras.RemoteInterface
{
    public interface ILibrosService
    {

        Task<(bool resultado, LibroRemote Libro, string ErrorMessage)> GetLibro(Guid LibroId);

    }

}
