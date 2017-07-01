using System;
using CrudRest;
using ServiceStack;
using ServiceStack.Text;

namespace CrudRest
{
    class Program
    {
        public static void Main()
        {
            // Inicializa la instancia del host de servicios
            var appHost = new AppHost();
            appHost.Init();

            // Arranca el host de servicios en el puerto 1337
            appHost.Start("http://*:1337/");

            using (var client = new JsonServiceClient("http://localhost:1337/"))
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

                persona = client.Post(persona);
                persona.PrintDump();

                persona = client.Get(persona);
                persona.PrintDump();

                persona = client.Put(persona);
                persona.PrintDump();

                client.Delete(persona);
                
                //persona = client.Get(persona);
            }

            Console.ReadKey();
        }
    }
}