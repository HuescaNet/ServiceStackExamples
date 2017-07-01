using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Logging;
using ServiceStack.OrmLite;

namespace Logging
{
    public class ServicioPersona : Service
    {
        static ILog Log = LogManager.GetLogger(typeof(ServicioPersona));

        public async Task<Persona> Get(Persona persona)
        {
            Log.Debug("Llamada a servicio GET Persona");
            persona = await Db.SingleByIdAsync<Persona>(persona.Id);
            return persona;
        }

        public async Task<Persona> Post(Persona persona)
        {
            Log.Debug("Llamada a servicio POST Persona");
            await Db.SaveAsync(persona);
            return persona;
        }

        public async Task<Persona> Put(Persona persona)
        {
            Log.Debug("Llamada a servicio PUT Persona");
            await Db.SaveAsync(persona);
            return persona;
        }

        public async Task Delete(Persona persona)
        {
            Log.Debug("Llamada a servicio DELETE Persona");
            await Db.DeleteAsync(persona);
        }
    }
}
