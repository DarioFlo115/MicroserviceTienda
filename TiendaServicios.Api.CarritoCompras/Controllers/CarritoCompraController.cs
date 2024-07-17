using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.CarritoCompras.Aplicacion;
using TiendaServicios.Api.CarritoCompras.Modelo;

namespace TiendaServicios.Api.CarritoCompras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoCompraController : ControllerBase
    {
        private readonly IMediator mediator;

        public CarritoCompraController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await mediator.Send(data);
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<CarritoDto>> GetCarrito(int id)
        {
            return await mediator.Send(new Consulta.Ejecuta
            {
                CarritoSesionId = id
            });
        }
    }
}
