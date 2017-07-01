using System;

namespace AutoQuery
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

            Console.ReadKey();
        }
    }
}