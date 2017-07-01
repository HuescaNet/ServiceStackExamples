using System;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Logging
{
    public class Persona : IReturn<Persona>
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
