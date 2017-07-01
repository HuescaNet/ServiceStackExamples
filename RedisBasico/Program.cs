using System;
using ServiceStack.Redis;
using ServiceStack.Text;

namespace RedisBasico
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
                // Almacena en Redis un par clave - valor
                redis.Add("ejemplos:clave", "valor");

                // Devuelve un valor a partir de la clave
                var valor = redis.Get<string>("ejemplos:clave");
                valor.PrintDump();

                // Añade varios elementos a una lista
                redis.AddItemToList("ejemplos:lista", "elemento1");
                redis.AddItemToList("ejemplos:lista", "elemento2");
                redis.AddItemToList("ejemplos:lista", "elemento3");

                // Devuelve todos los elementos de una lista
                var elementosDeLaLista = redis.GetAllItemsFromList("ejemplos:lista");
                elementosDeLaLista.PrintDump();

                // Elimina un elemento de la lista
                redis.RemoveItemFromList("ejemplos:lista", "elemento2");

                // Devuelve todos los elementos de una lista
                elementosDeLaLista = redis.GetAllItemsFromList("ejemplos:lista");
                elementosDeLaLista.PrintDump();

                // Elimina todos los elementos de la lista
                redis.RemoveAllFromList("ejemplos:lista");

                // Añade elementos a un set, si uno se repite no se vuelve a añadir
                redis.AddItemToSet("ejemplos:set", "elemento1");
                redis.AddItemToSet("ejemplos:set", "elemento2");
                redis.AddItemToSet("ejemplos:set", "elemento3");
                redis.AddItemToSet("ejemplos:set", "elemento1");

                // Devuelve todos los elementos de un set
                var elementosDelSet = redis.GetAllItemsFromSet("ejemplos:set");
                elementosDelSet.PrintDump();

                // Elimina un elemento de un set
                redis.RemoveItemFromSet("ejemplos:set", "elemento2");

                // Devuelve todos los elementos de un set
                elementosDelSet = redis.GetAllItemsFromSet("ejemplos:set");
                elementosDelSet.PrintDump();

                Console.ReadLine();
            }
        }
    }
}