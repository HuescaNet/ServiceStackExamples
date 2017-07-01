using System;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace OpenAPI
{
    [Api("Servicio REST de personas")]
    [Route("/Persona/{Id}", "GET", Summary = "Obtiene los datos de una persona por Id", Notes = "Notas sobre como obtener datos de persona")]
    [Route("/Persona", "POST", Summary = "Inserta los datos de una persona", Notes = "Notas sobre como insertar datos de persona")]
    [Route("/Persona", "PUT", Summary = "Actualiza los datos de una persona", Notes = "Notas sobre como actualizar datos de persona")]
    [Route("/Persona", "DELETE", Summary = "Elimina los datos de una persona", Notes = "Notas sobre como eliminar datos de persona")]
    public class Persona : IReturn<Persona>
    {
        [AutoIncrement]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        public DateTime? FechaNacimiento { get; set; }
    }
}
