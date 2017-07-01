using System;
using ServiceStack.OrmLite;
using ServiceStack.Text;

namespace Filtros
{
    class Program
    {
        public static void Main()
        {
            // Factoría para crear instancias de conexiones a base de datos configurada 
            // para utilizar el dialecto de SQLServer
            var dbFactory = new OrmLiteConnectionFactory(
                "Data Source=localhost\\SQLExpress;Initial Catalog=ServiceStackExamples;Integrated Security=SSPI",
                SqlServerDialect.Provider);

            // Filtro de cadena que quita espacios y pasa a mayúsculas
            OrmLiteConfig.StringFilter = x => x?.Trim().ToUpperInvariant();

            // Filtro de inserción para establecer fechas de creación y modificación
            OrmLiteConfig.InsertFilter = (command, poco) =>
            {
                if (poco is IAuditable auditable)
                {
                    auditable.FechaCreacion = DateTime.Now;
                    auditable.FechaUltimaModificacion = DateTime.Now;
                    auditable.Usuario = " usuario   ";
                }
            };

            // Filtro de actualizado para establecer fechas de creación y modificación
            OrmLiteConfig.UpdateFilter = (command, poco) =>
            {
                if (poco is IAuditable auditable)
                {
                    auditable.FechaUltimaModificacion = DateTime.Now;
                    auditable.Usuario = " Otro Usuario       ";
                }
            };

            // Crea y abre una nueva conexión a la base de datos
            using (var db = dbFactory.Open())
            {
                // Elimina y vuelve a crear la tabla definida por el POCO
                db.DropAndCreateTable<PocoAuditable>();

                // Crea instancia del POCO
                var poco = new PocoAuditable();

                poco.PrintDump();

                // Guarda y vuelve a cargar, hace saltar el filtro de inserción
                db.Save(poco);
                poco = db.SingleById<PocoAuditable>(poco.Id);
                
                poco.PrintDump();

                // Guarda y vuelve a cargar, hace saltar el filtro de actualización
                db.Save(poco);
                poco = db.SingleById<PocoAuditable>(poco.Id);

                poco.PrintDump();
            }

            Console.ReadLine();
        }
    }
}