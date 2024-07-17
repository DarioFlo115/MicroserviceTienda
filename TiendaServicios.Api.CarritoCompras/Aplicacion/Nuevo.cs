using AutoMapper;
using MediatR;
using TiendaServicios.Api.CarritoCompras.Modelo;
using TiendaServicios.Api.CarritoCompras.Persistencia;

namespace TiendaServicios.Api.CarritoCompras.Aplicacion
{
    public class Nuevo 
    {

        public class Ejecuta : IRequest<Unit>
        {
            public DateTime? FechaCreacionSesion { get; set; }

            public List<string>? ProductoLista { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, Unit>
        {
            private readonly ContextoCarritoCompras contexto;
            private readonly IMapper mapper;

            public Manejador(ContextoCarritoCompras contexto,
                             IMapper mapper)
            {
                this.contexto = contexto;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacionSesion
                };

                contexto.CarritoSesion.Add(carritoSesion);
                var value = await contexto.SaveChangesAsync();

                if (value == 0)
                {
                    throw new Exception("Errores en la incersion del carrito de compras");
                }

                int id = carritoSesion.CarritoSesionId;

                foreach (var obj in request.ProductoLista)
                {
                    var detalleSesion = new CarritoSesionDetalle
                    {
                        FechaCracion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSeleccionado = obj
                    };

                   contexto.CarritoSesionDetalle.Add(detalleSesion);
                }

                value = await contexto.SaveChangesAsync();

                if (value > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el detalle del carrito de compras");

            }
        }

    }
}
