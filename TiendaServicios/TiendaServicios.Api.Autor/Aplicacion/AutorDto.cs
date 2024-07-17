namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class AutorDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        //Un guid es una clave unica para darle seguimiento a AutorLibro desde otro microservicio (Global Unique Identifier)
        public string AutorLibroGuid { get; set; }
    }
}
