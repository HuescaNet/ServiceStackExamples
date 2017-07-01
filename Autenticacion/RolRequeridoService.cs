using ServiceStack;

namespace Autenticacion
{
    [RequiredRole("Administrador")]
    public class RolRequeridoService : Service
    {
        public RequiresRoleResponse Any(RolRequeridoRequest request)
        {
            return new RequiresRoleResponse { Dato = request.Dato };
        }
    }
}