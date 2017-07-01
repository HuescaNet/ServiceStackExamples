using System;
using ServiceStack;

namespace ServerEventsServidor
{
    class Program
    {
        static void Main(string[] args)
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