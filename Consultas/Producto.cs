using ServiceStack.DataAnnotations;

namespace Consultas
{
    public class Producto
    {
        [AutoIncrement]
        public long Id { get; set; }
        public string Nombre { get; set; }
    }
}