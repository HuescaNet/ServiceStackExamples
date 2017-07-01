using System;
using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace Referencias
{
    class Factura
    {
        public Factura()
        {
            Lineas = new List<LineaFactura>();
        }

        [AutoIncrement]
        public long Id { get; set; }

        public string Numero { get; set; }

        public DateTime Fecha { get; set; }

        [Reference]
        public List<LineaFactura> Lineas { get; set; }
    }
}