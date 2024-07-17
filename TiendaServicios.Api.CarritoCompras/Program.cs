using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarritoCompras.Aplicacion;
using TiendaServicios.Api.CarritoCompras.Persistencia;
using TiendaServicios.Api.CarritoCompras.RemoteInterface;
using TiendaServicios.Api.CarritoCompras.RemoteService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Nuevo.Manejador));
builder.Services.AddScoped<ILibrosService, LibrosService>();
builder.Services.AddHttpClient("Libros", config => {
    config.BaseAddress = new Uri(builder.Configuration["Services:Libros"]);
});
builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
builder.Services.AddDbContext<ContextoCarritoCompras>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("ConexionDatabase")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
