using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CrudRest;
using ServiceStack;
using ServiceStack.OrmLite;

namespace CrudRest
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
    }
}
