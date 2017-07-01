using System;
using Funq;
using ServiceStack;
using ServiceStack.Admin;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace AutoQuery
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
                new OrmLiteConnectionFactory("Data Source=localhost\\SQLExpress;Initial Catalog=WideWorldImporters;Integrated Security=SSPI", SqlServerDialect.Provider));

            // Configura la característica AutoQuery con un límite de registros de 100
            Plugins.Add(new AutoQueryFeature { MaxLimit = 100 });
            // Configura la característica de administración de AutoQuery
            Plugins.Add(new AdminFeature());
        }
    }
}