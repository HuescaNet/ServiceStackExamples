using System;
using ServerEventsServidor;
using ServiceStack;
using ServiceStack.Text;

namespace ServerEventsCliente
{
    class Program
    {
        public static void Main()
        {
            Console.Write("Nombre de usuario:");
            var usuario = Console.ReadLine();

            using (var client = new ServerEventsClient("http://localhost:1337/"))
            {

                client.OnConnect = e => Console.WriteLine("Conectado al servidor");
                client.OnMessage = e =>
                {
                    var mensaje = e.Json.FromJson<MensajeRequest>();
                    if (mensaje.Usuario != usuario)
                    {
                        Console.WriteLine($"Mensaje recibido: {e.Json.FromJson<MensajeRequest>().Usuario} => {e.Json.FromJson<MensajeRequest>().Mensaje}");
                    }
                };

                client.Connect();

                while (true)
                {
                    var mensaje = Console.ReadLine();
                    client.ServiceClient.Post(new MensajeRequest { Usuario = usuario, Mensaje = mensaje });
                }
            }
        }
    }
}