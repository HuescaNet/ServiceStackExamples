using System;
using ServiceStack;

namespace Validacion
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
                    Nombre = "",
                    Direccion = new Direccion
                    {
                        Calle = "Mi calle",
                        Numero = 6,
                        Piso = "1º"
                    },
                    FechaNacimiento = new DateTime(1981, 10, 28)
                };

                try
                {
                    client.Post(persona);
                }
                // Falla la primera validación por nombre vacío
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                persona.Nombre = "Jorge";
                // Inserta una persona
                client.Post(persona);

                try
                {
                    client.Post(persona);
                }
                // Falla la segunda validación por nombre duplicado
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            Console.ReadKey();
        }
    }
}