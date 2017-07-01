using System;
using ServiceStack.Redis;

namespace HyperLogLog
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
                for (int i = 0; i < 3000; i++)
                {
                    // Registra un usuario como elemento del HyperLogLog
                    redis.AddToHyperLog("ejemplos:numeroUsuariosUnicos", $"usuario{i}");
                }

                // Consulta el número de elementos únicos aproximados en el HyperLogLog
                var usuariosUnicos = redis.CountHyperLog("ejemplos:numeroUsuariosUnicos");
                Console.WriteLine($"Número de usuarios únicos aproximado: {usuariosUnicos}");

                Console.WriteLine("Volviendo a añadir los mismos usuarios");

                for (int i = 0; i < 3000; i++)
                {
                    // Registra un usuario como elemento del HyperLogLog
                    redis.AddToHyperLog("ejemplos:numeroUsuariosUnicos", $"usuario{i}");
                }

                // Consulta el número de elementos únicos aproximados en el HyperLogLog
                usuariosUnicos = redis.CountHyperLog("ejemplos:numeroUsuariosUnicos");
                redis.AddToHyperLog("ejemplos:numeroUsuariosUnicos", "asdfasdf");
                redis.AddToHyperLog("ejemplos:numeroUsuariosUnicos", "qwerqwer");
                Console.WriteLine($"Número de usuarios únicos aproximado: {usuariosUnicos}");
                Console.ReadLine();
            }
        }
    }
}