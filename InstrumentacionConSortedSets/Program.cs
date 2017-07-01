using System;
using System.Runtime.CompilerServices;
using System.Threading;
using ServiceStack.Redis;

namespace InstrumentacionConSortedSets
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
            // Incrementa el score del item 'operacion' del set 'operacionesConSortedSets:tiempo'
            redis.IncrementItemInSortedSet("operacionesConSortedSets:tiempo", operacion, tiempo);
            // Incrementa el score del item 'operacion' del set 'operacionesConSortedSets:ejecuciones'
            redis.IncrementItemInSortedSet("operacionesConSortedSets:ejecuciones", operacion, 1);
        }

        static void MostrarResultados(IRedisClient redis)
        {
            // Obtiene todos los items y scores del set 'operacionesConSortedSets:tiempo'
            var operacionesPorTiempo = redis.GetAllWithScoresFromSortedSet("operacionesConSortedSets:tiempo");
            // Obtiene todos los items y scores del set 'operacionesConSortedSets:ejecuciones'
            var operacionesPorNumeroDeEjecuciones = redis.GetAllWithScoresFromSortedSet("operacionesConSortedSets:ejecuciones");

            Console.Clear();
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Operaciones por tiempo");
            Console.WriteLine("-----------------------------------------");
            foreach (var operacion in operacionesPorTiempo)
            {
                Console.WriteLine($"{operacion.Key} => {operacion.Value} ms");
            }
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Operaciones por numero de ejecuciones");
            Console.WriteLine("-----------------------------------------");
            foreach (var operacion in operacionesPorNumeroDeEjecuciones)
            {
                Console.WriteLine($"{operacion.Key} => {operacion.Value} ejecuciones");
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