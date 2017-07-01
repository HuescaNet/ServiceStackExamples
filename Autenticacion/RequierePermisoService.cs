using ServiceStack;

namespace Autenticacion
{
    [RequiredPermission("EnviarPeticiones")]
    public class RequierePermisoService : Service
    {
        public RequierePermisoResponse Any(RequierePermisoRequest request)
        {
            return new RequierePermisoResponse { Dato = request.Dato };
        }
    }
}