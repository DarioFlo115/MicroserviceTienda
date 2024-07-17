namespace TiendaServicios.Api.CarritoCompras.RemoteModel
{
    public class LibroRemote
    {
        //Esto es el mismo modelo de TiendaServicios.Api.Libro.Modelo.LibreriaMaterial solo es copiado
        public Guid? LibreriaMaterialId { get; set; }
        public string? Titulo { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public Guid? AutorLibro { get; set; }

    }
}
