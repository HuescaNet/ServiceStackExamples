using System;
using ServiceStack.DataAnnotations;

namespace Consultas
{
    class Factura
    {
        [AutoIncrement]
        public long Id { get; set; }

        public string Numero { get; set; }

        public DateTime Fecha { get; set; }
    }
}