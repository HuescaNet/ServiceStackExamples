using ServiceStack.DataAnnotations;

namespace Referencias
{
    class LineaFactura
    {
        [AutoIncrement]
        public long Id { get; set; }

        [References(typeof(Factura))]
        public long FacturaId { get; set; }

        public string Producto { get; set; }
        public decimal Precio { get; set; }
        public int Unidades { get; set; }
    }
}