using System;
using System.Linq;
using ServiceStack.Redis;
using ServiceStack.Text;

namespace SesionConRedisTipado
{
    class Program
    {
        public static void Main()
        {
            // Factoría para crear conexiones a instancia de Redis
            // BasicRedisClientManager abre nueva conexión por cada operación
            // PooledRedisClientManager mantiene conexión abierta para mejorar
            // el rendimiento en instancias de Redis remotas
            using (var redisManager = new BasicRedisClientManager())
            // Crea conexión a instancia de Redis
            using (var redis = redisManager.GetClient())
            {
                // Crea un cliente tipado para gestionar la clase Sesion
                var redisSesion = redis.As<Sesion>();

                var sesion = new Sesion
                {
                    Id = redisSesion.GetNextSequence(),
                    NombreUsuario = "Jorge",
                    ZonaHoraria = "Europe/Madrid",
                    Idioma = "en-US"
                };

                // Almacena la sesión en la instancia de Redis
                redisSesion.Store(sesion);

                // Obtiene la sesión de la instancia de Redis por Id
                var sesionGuardada = redisSesion.GetById(sesion.Id);

                // Muestra la sesión
                sesionGuardada.PrintDump();

                // Modifica un valor de la sesión y la vuelve a guardar
                sesionGuardada.Idioma = "es-ES";
                redisSesion.Store(sesionGuardada);

                // Vuelve a obtener la sesión de la instancia de Redis por Id
                sesionGuardada = redisSesion.GetById(sesionGuardada.Id);
                // Muestra la sesión
                sesionGuardada.PrintDump();

                // Obtiene y muestra todas las sesiones
                redisSesion.GetAll().ToList().PrintDump();

                // Establece un tiempo de expiración a la sesión
                redisSesion.ExpireAt(sesionGuardada.Id, DateTime.Now.AddSeconds(10));

                // Elimina la sesión por Id
                redisSesion.DeleteById(sesionGuardada.Id);
            }

            Console.ReadLine();
        }
    }
}