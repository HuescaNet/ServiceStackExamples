using System;
using OpenAPI;
using Funq;
using ServiceStack;
using ServiceStack.Admin;
using ServiceStack.Api.OpenApi;
using ServiceStack.Data;
using ServiceStack.Logging;
using ServiceStack.OrmLite;

namespace OpenAPI
{
    // Clase host de los servicios
    public class AppHost : AppSelfHostBase
    {
        // Configura el host con todos los servicios que se hayan definido en en el mismo assembly
        public AppHost() : base("Servicio", typeof(AppHost).GetAssembly()) { }

        // Configuraci�n del host
        public override void Configure(Container container)
        {            
            // Configura el proveedor de base de datos y la cadena de conexi�n
            container.Register<IDbConnectionFactory>(
                new OrmLiteConnectionFactory("Data Source=localhost\\SQLExpress;Initial Catalog=WideWorldImporters;Integrated Security=SSPI", SqlServerDialect.Provider));

            // Crea y abre una nueva conexi�n a la base de datos
            using (var db = container.Resolve<IDbConnectionFactory>().Open())
            {
                db.DropAndCreateTable<Persona>();
            }


            Plugins.Add(new OpenApiFeature()); 
        }
    }
}