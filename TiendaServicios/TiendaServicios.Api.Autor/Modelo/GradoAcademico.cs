namespace TiendaServicios.Api.Autor.Modelo
{
    public class GradoAcademico
    {
        public int GradoAcademicoId { get; set; }
        public string Nombre { get; set; }
        public string CentroAcademico { get; set; }
        public DateTime? FechaGrado { get; set; }

        ///Aqui estoy haciendo una relacion una a muchos entre Autor y grado academico por si el autor tiene varios grados academicos
        public int AutorLibroId { get; set; }
        public AutorLibro AutorLibro { get; set; }


        //Un guid es una clave unica para darle seguimiento a AutorLibro desde otro microservicio (Global Unique Identifier)
        public string GradoAcademicoGuid { get; set; }

    }
}
