using ServiceStack;

namespace DI
{
    class Servicio : IService
    {
        // Dependencias por constructor
        private readonly IDependenciaPorPeticion dependenciaPorPeticion;
        private readonly IDependenciaPorContenedor dependenciaPorContenedor;

        public Servicio(IDependenciaPorPeticion dependenciaPorPeticion, IDependenciaPorContenedor dependenciaPorContenedor)
        {
            this.dependenciaPorPeticion = dependenciaPorPeticion;
            this.dependenciaPorContenedor = dependenciaPorContenedor;
        }

        // Dependencias por propiedad pública
        //public IDependenciaPorPeticion dependenciaPorPeticion { get; set; }
        //public IDependenciaPorContenedor dependenciaPorContenedor { get; set; }

        public string Any(RequestPorPeticion request)
        {
            return dependenciaPorPeticion.Mensaje();
        }

        public string Any(RequestPorContenedor request)
        {
            return dependenciaPorContenedor.Mensaje();
        }
    }
}