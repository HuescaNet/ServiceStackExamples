using System;
using ServiceStack.DataAnnotations;

namespace Filtros
{
    class PocoAuditable : IAuditable
    {
        [AutoIncrement]
        public long Id { get; set; }

        public string Usuario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
    }
}