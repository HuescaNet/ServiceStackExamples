using System;
using System.Runtime.CompilerServices;
using System.Threading;
using ServiceStack;
using ServiceStack.Redis;

namespace InstrumentacionConHashes
{
    class Program
    {
        public static void Main()
        {
            // Factoría para crear conexiones a instancia de Redis
            // BasicRedisClientManager abre nueva conexión por cada operación
            // PooledRedisClientManager mantiene conexión abierta para mejorar
            // el rendimiento en instancias de Redis remotas
            using (var redisManager = new PooledRedisClientManager())
                // Crea conexión a instancia de Redis
            using (var redis = redisManager.GetClient())
            {
                var random = new Random();
                for (int j = 0; j < 50; j++)
                {
                    // Llama aleatoriamente a una de las funciones instrumentadas
                    for (int i = 0; i < 100; i++)
                    {
                        var aleatorio = random.Next() % 3;
                        switch (aleatorio)
                        {
                            case 0:
                                ConsultaABaseDeDatos(redis);
                                break;
                            case 1:
                                ConsultaAServicioWeb(redis);
                                break;
                            default:
                                OperacionDeRedis(redis);
                                break;
                        }
                    }
                    // Muestra los resultados de las funciones instrumentadas
                    MostrarResultados(redis);
                    Thread.Sleep(1000);
                }
            }
            Console.ReadLine();
        }

        static void RegistrarEnRedis(IRedisClient redis, long tiempo, [CallerMemberName] string operacion = "")
        {
            // Incrementa el valor de la propiedad 'tiempo' del hash 'operacionesConHashes:{operacion}'
            redis.IncrementValueInHash("operacionesConHashes:" + operacion, "tiempo", tiempo);
            // Incrementa el valor de la propiedad 'ejecuciones' del hash 'operacionesConHashes:{operacion}'
            redis.IncrementValueInHash("operacionesConHashes:" + operacion, "ejecuciones", 1);
        }

        static void MostrarResultados(IRedisClient redis)
        {
            // Obtiene todos los items y scores del set 'operacionesConHashes:tiempo'
            var operaciones = redis.ScanAllKeys("operacionesConHashes:*");

            Console.Clear();
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Operaciones");
            Console.WriteLine("-----------------------------------------");
            foreach (var operacion in operaciones)
            {
                Console.WriteLine(operacion);
                foreach (var propiedad in redis.GetAllEntriesFromHash(operacion))
                {
                    Console.WriteLine($"\t{propiedad.Key} => {propiedad.Value}");
                }
            }
        }

        // Método simulado a instrumentar
        static void ConsultaABaseDeDatos(IRedisClient redis)
        {
            var random = new Random();
            RegistrarEnRedis(redis, random.Next(50, 200));
        }

        // Método simulado a instrumentar
        static void ConsultaAServicioWeb(IRedisClient redis)
        {
            var random = new Random();
            RegistrarEnRedis(redis, random.Next(150, 500));
        }

        // Método simulado a instrumentar
        static void OperacionDeRedis(IRedisClient redis)
        {
            var random = new Random();
            RegistrarEnRedis(redis, random.Next(10, 20));
        }
    }
}