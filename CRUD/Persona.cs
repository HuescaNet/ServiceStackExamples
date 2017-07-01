using System;
using ServiceStack.DataAnnotations;

namespace CRUD
{
    [Alias("Personas")]
    public class Persona
    {
        [AutoIncrement]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public Direccion Direccion { get; set; }
    }
}
