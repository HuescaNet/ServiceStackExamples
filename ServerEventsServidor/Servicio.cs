using System;
using ServiceStack;
using ServiceStack.Logging;

namespace ServerEventsServidor
{
    class Servicio : IService
    {
        IServerEvents ServerEvents { get; set; }
        ILog Log = LogManager.GetLogger(typeof(Servicio));

        public void Any(MensajeRequest request)
        {
            Log.Debug($"SERVIDOR: Mensaje recibido: {request?.Mensaje}");
            ServerEvents.NotifyAll(request);
        }
    }
}