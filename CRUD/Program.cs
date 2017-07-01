using System;
using ServiceStack.OrmLite;
using ServiceStack.Text;

namespace CRUD
{
    class Program
    {
        public static void Main()
        {
            // Factoría para crear instancias de conexiones a base de datos configurada 
            // para utilizar el dialecto de SQLite y funcionar en memoria
            //var dbFactory = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider);

            // Factoría para crear instancias de conexiones a base de datos configurada 
            // para utilizar el dialecto de SQLServer
            var dbFactory = new OrmLiteConnectionFactory(
                "Data Source=localhost\\SQLExpress;Initial Catalog=ServiceStackExamples;Integrated Security=SSPI",
                SqlServerDialect.Provider);

            // Crea y abre una nueva conexión a la base de datos
            using (var db = dbFactory.Open())
            {
                // Elimina y vuelve a crear la tabla definida por el POCO
                db.DropAndCreateTable<Persona>();

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

                // Guarda en base de datos la instancia de la clase Persona
                db.Save(persona);

                // Recupera de base de datos por Id
                var personaEncontrada = db.SingleById<Persona>(persona.Id);
                personaEncontrada.PrintDump();
            }

            Console.ReadLine();
        }
    }
}