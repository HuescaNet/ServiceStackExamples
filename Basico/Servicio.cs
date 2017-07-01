using System;
using System.IO;
using ServiceStack;

namespace Basico
{
    class Servicio : IService
    {
        public string Any(StringRequest request)
        {
            return "Hola mundo";
        }

        public object Any(PocoRequest request)
        {
            var persona = new Persona
            {
                Nombre = "Jorge",
                Direccion = new Direccion
                {
                    Calle = "Mi calle",
                    Numero = 6,
                    Piso = "1º"
                },
                FechaNacimiento = new DateTime(1981, 10, 28)
            };

            return persona;
        }

        public object Any(StreamRequest request)
        {
            return new HttpResult(new FileInfo(@"C:\Working\ServiceStack - Acceso a datos.pdf"));
        }
    }
}