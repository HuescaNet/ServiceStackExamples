using ServiceStack.DataAnnotations;

namespace Consultas
{
    class LineaFactura
    {
        [AutoIncrement]
        public long Id { get; set; }

        [References(typeof(Factura))]
        public long FacturaId { get; set; }

        [References(typeof(Producto))]
        public long ProductoId { get; set; }

        public decimal Precio { get; set; }
        public int Unidades { get; set; }
    }
}