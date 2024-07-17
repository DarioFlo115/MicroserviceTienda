﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class ConsultaFiltro 
    {
        public class AutorUnico : IRequest<AutorDto>
        {
            public string? AutorGuid { get; set; }
        }
        public class Manejador : IRequestHandler<AutorUnico, AutorDto>
        {

            private readonly ContextoAutor contexto;
            private readonly IMapper mapper;

            public Manejador(ContextoAutor contexto,
                             IMapper mapper)
            {
                this.contexto = contexto;
                this.mapper = mapper;
            }


            public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await contexto.AutorLibro.Where(x => x.AutorLibroGuid == request.AutorGuid).FirstOrDefaultAsync();
                if (autor == null)
                {
                    throw new Exception("No se encontro el autor");
                }
                var autorDto = mapper.Map<AutorLibro, AutorDto>(autor);

                return autorDto;
            }
        }


    }
}
