using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarritoCompras.Modelo;

namespace TiendaServicios.Api.CarritoCompras.Persistencia
{
    public class ContextoCarritoCompras : DbContext
    {
        public ContextoCarritoCompras(DbContextOptions<ContextoCarritoCompras> options) : base(options)
        {
            
        }
        //dbsets
        public DbSet<CarritoSesion> CarritoSesion { get; set; }
        public DbSet<CarritoSesionDetalle> CarritoSesionDetalle { get; set; }
    }
}
