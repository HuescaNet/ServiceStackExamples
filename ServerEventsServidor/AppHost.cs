using Funq;
using ServiceStack;
using ServiceStack.Logging;

namespace ServerEventsServidor
{
    // Clase host de los servicios
    public class AppHost : AppSelfHostBase
    {
        // Configura el host con todos los servicios que se hayan definido en en el mismo assembly
        public AppHost() : base("Servicio", typeof(AppHost).GetAssembly()) { }

        // Configuraci�n del host
        public override void Configure(Container container)
        {
            // Registra la implementaci�n de Log que muestra los mensajes por consola
            LogManager.LogFactory = new ConsoleLogFactory();

            Plugins.Add(new ServerEventsFeature());
        }
    }
}