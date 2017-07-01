using ServiceStack;

namespace ServerEventsServidor
{
    public class MensajeRequest :IReturnVoid
    {
        public string Usuario { get; set; }
        public string Mensaje { get; set; }
    }    
}