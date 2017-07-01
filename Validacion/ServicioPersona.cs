using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Validacion;
using ServiceStack;
using ServiceStack.OrmLite;

namespace Validacion
{
    public class ServicioPersona : Service
    {
        public async Task<Persona> Get(Persona persona)
        {
            persona = await Db.SingleByIdAsync<Persona>(persona.Id);
            return persona;
        }

        public async Task<Persona> Post(Persona persona)
        {
            await Db.SaveAsync(persona);
            return persona;
        }

        public async Task<Persona> Put(Persona persona)
        {
            await Db.SaveAsync(persona);
            return persona;
        }

        public async Task Delete(Persona persona)
        {
            await Db.DeleteAsync(persona);
        }

        public bool Post(CompruebaNombreRequest request)
        {
            return !Db.Exists<Persona>(p => p.Nombre == request.Nombre);
        }
    }
}
