using NodaTime;
using ServiceStack.DataAnnotations;

namespace Conversores
{
    [Alias("ProductoConversores")]
    public class Producto
    {
        [AutoIncrement]
        public long Id { get; set; }
        public string Nombre { get; set; }

        public Color Color { get; set; }

        public ZonedDateTime UltimaEntrada { get; set; }
    }
}