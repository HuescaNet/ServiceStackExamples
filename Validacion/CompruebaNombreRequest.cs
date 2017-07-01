using ServiceStack;

namespace Validacion
{
    public class CompruebaNombreRequest : IReturn<bool>
    {
        public string Nombre { get; set; }
    }
}