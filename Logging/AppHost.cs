using Funq;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.Logging;
using ServiceStack.OrmLite;

namespace Logging
{
    // Clase host de los servicios
    public class AppHost : AppSelfHostBase
    {
        // Configura el host con todos los servicios que se hayan definido en en el mismo assembly
        public AppHost() : base("Servicio", typeof(AppHost).GetAssembly()) { }

        // Configuración del host
        public override void Configure(Container container)
        {
            // Configura el proveedor de base de datos y la cadena de conexión
            container.Register<IDbConnectionFactory>(
                new OrmLiteConnectionFactory("Data Source=localhost\\SQLExpress;Initial Catalog=ServiceStackExamples;Integrated Security=SSPI", SqlServerDialect.Provider));

            // Crea y abre una nueva conexión a la base de datos
            using (var db = container.Resolve<IDbConnectionFactory>().Open())
            {
                db.DropAndCreateTable<Persona>();
            }

            // Registra la implementación de Log que muestra los mensajes por consola
            LogManager.LogFactory = new ConsoleLogFactory();

            // Registra un filtro global que loguea cada petición que se hace
            //GlobalRequestFilters.Add((req, res, requestDto) => {
            //    LogManager.GetLogger(req.GetType()).Debug($"GLOBAL - Llamada a servicio {req.Verb} {requestDto.GetType().Name}" );
            //});
        }
    }
}