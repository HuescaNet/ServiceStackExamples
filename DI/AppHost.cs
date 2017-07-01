using Funq;
using ServiceStack;

namespace DI
{
    // Clase host de los servicios
    public class AppHost : AppSelfHostBase
    {
        // Configura el host con todos los servicios que se hayan definido en en el mismo assembly
        public AppHost() : base("Servicio", typeof(AppHost).GetAssembly()) { }

        // Configuración del host
        public override void Configure(Container container)
        {
            // Registro de dependencias en el contenedor de Funq
            // Dependencia por petición
            container.RegisterAs<DependenciaPorPeticion, IDependenciaPorPeticion>().ReusedWithin(ReuseScope.Request);
            // Dependencia por contenedor
            container.RegisterAs<DependenciaPorContenedor, IDependenciaPorContenedor>().ReusedWithin(ReuseScope.Container);
        }
    }
}