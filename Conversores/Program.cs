using System;
using NodaTime;
using ServiceStack.Logging;
using ServiceStack.OrmLite;
using ServiceStack.Text;

namespace Conversores
{
    class Program
    {
        public static void Main()
        {
            // Factoría para crear instancias de conexiones a base de datos configurada 
            // para utilizar el dialecto de SQLServer
            var dbFactory = new OrmLiteConnectionFactory("Data Source=localhost\\SQLExpress;Initial Catalog=ServiceStackExamples;Integrated Security=SSPI", SqlServerDialect.Provider);

            // Registra los conversores en el proveedor utilizado
            SqlServerDialect.Provider.RegisterConverter<Color>(new ColorConverter("int"));
            SqlServerDialect.Provider.RegisterConverter<ZonedDateTime>(new ZonedDateTimeConverter());

            // Configura el sistema de log para mostrar las SQL generadas
            LogManager.LogFactory = new ConsoleLogFactory();

            // Crea y abre conexión a base de datos
            using (var db = dbFactory.Open())
            {
                // Elimina y crea la tabla a partir del POCO
                db.DropAndCreateTable<Producto>();

                // Crea una instancia de producto
                var producto = new Producto();
                producto.Nombre = "Nombre del producto";
                producto.UltimaEntrada = new ZonedDateTime(SystemClock.Instance.GetCurrentInstant(), DateTimeZoneProviders.Tzdb["Europe/Madrid"]);
                producto.Color = Color.Rojo;

                producto.PrintDump();

                // Guarda la instancia en la base de datos
                db.Save(producto);

                // Carga la instancia desde base de datos
                producto = db.SingleById<Producto>(producto.Id);
                producto.Dump();

                var query = db.From<Producto>().Where(x => x.Color == Color.Rojo);

                var productosEnRojo = db.Select(query);
                productosEnRojo.PrintDump();
            }

            Console.ReadLine();
        }
    }
}