using FluentValidation;
using MediatR;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest<Unit>
        {
            public string Titulo { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public Guid? AutorLibro { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
                RuleFor(x => x.AutorLibro).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta, Unit>
        {
            private readonly ContextoLibreria contexto;

            public Manejador(ContextoLibreria contexto)
            {
                this.contexto = contexto;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libro = new LibreriaMaterial
                {
                    Titulo = request.Titulo,
                    AutorLibro = request.AutorLibro,
                    FechaPublicacion = request.FechaPublicacion
                };

                contexto.LibreriaMaterial.Add(libro);
                //Si el proceso hasta este punto es exitoso el await contexto.SaveChangesAsync() va a retornar una variable de valor 1 de lo contrario va a retornar un 0
                var value = await contexto.SaveChangesAsync();

                if (value > 0)
                {
                    //Preguntar o investigar para que se ocupa el Unit aqui
                    return Unit.Value;
                }

                throw new Exception("No se pudo guardar el libro ");

                
            }
        }
    }
}
