using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;
using Xunit;

namespace TiendaServicios.Api.Libro.Tests
{
    public class LibrosServiceTest
    {
        private IEnumerable<LibreriaMaterial> ObtenerDataPrueba()
        {
            A.Configure<LibreriaMaterial>().Fill(x => x.Titulo).AsArticleTitle().Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<LibreriaMaterial>(30);
            lista[0].LibreriaMaterialId = Guid.Empty;

            return lista;
        }

        


        private Mock<ContextoLibreria> CrearContexto()
        {
            var dataPrueba = ObtenerDataPrueba().AsQueryable();

            var dbSet = new Mock<DbSet<LibreriaMaterial>>();
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression); 
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken())).Returns(new AsyncEnumerator<LibreriaMaterial>(dataPrueba.GetEnumerator()));


            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibreriaMaterial>(dataPrueba.Provider));

            var contexto = new Mock<ContextoLibreria>();
            contexto.Setup(x => x.LibreriaMaterial).Returns(dbSet.Object);
            return contexto;
        }

        [Fact]
        public async void GetLibroPorId()
        {
            var mockcontexto = CrearContexto();
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();

            var request = new ConsultaFiltro.LibroUnico();
            request.LibroId = Guid.Empty;


            var manejador = new ConsultaFiltro.Manejador(mockcontexto.Object, mapper);

           var libro = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(libro);

            Assert.True(libro.LibreriaMaterialId == Guid.Empty);
        }


        [Fact]
        public async void GuardarLibro()
        {

            var options = new DbContextOptionsBuilder<ContextoLibreria>().UseInMemoryDatabase(databaseName : "BaseDatosLibro").Options;

            var contexto = new ContextoLibreria(options);

            var request = new Nuevo.Ejecuta();
            request.Titulo = "Libro de Microservice"; 
            request.AutorLibro = Guid.Empty;
            request.FechaPublicacion = DateTime.Now;


            var manejador = new Nuevo.Manejador(contexto);
            var libro = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(libro != null);
            Assert.NotNull(libro);

        }

        [Fact]
        public async void GetLibros()
        {
            //System.Diagnostics.Debugger.Launch(); este comando es para poder debuggear un test
            //Que metodo de microservice Libro se esta encargando de realizar la consulta de libros de la base de datos??


            // 1.- Emular a la instancia de EntityFramworkCore (O sea simular la funcionalidad de ContextoLibreria dentro de este ambiente de test)
            //Para emular las funcionalidades o las acciones y eventos de un objeto en el ambiente de test utilizamos objetod de tipo mock y necesitamos intalar el paquete de Nugget

            var mockContexto = CrearContexto();

            //2.- Emular el mapping IMapper

            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();
            //Instanciar a la clase manejador y pasarle como parametros los mocks que he creado

            Consulta.Manejador manejador = new Consulta.Manejador(mockContexto.Object, mapper);
            Consulta.Ejecuta request = new Consulta.Ejecuta();

            var lista = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(lista.Any());
        }
    }
}