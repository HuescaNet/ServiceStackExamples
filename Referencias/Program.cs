using System;
using ServiceStack.OrmLite;
using ServiceStack.Text;

namespace Referencias
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

            using (var db = dbFactory.Open())
            {
                // Elimina las tablas de la base de datos
                db.DropTable<LineaFactura>();
                db.DropTable<Factura>();

                // Crea las tablas a partir de la definición de los POCOs
                db.CreateTable<Factura>();
                db.CreateTable<LineaFactura>();

                // Crea una instancia de Factura
                var factura = new Factura
                {
                    Numero = "2017/001234",
                    Fecha = DateTime.Now
                };

                // Añade una línea de factura a la instancia anterior
                factura.Lineas.Add(new LineaFactura
                {
                    Producto = "Producto A",
                    Precio = 12.34m,
                    Unidades = 6
                });

                // Añade otra línea de factura a la instancia anterior
                factura.Lineas.Add(new LineaFactura
                {
                    Producto = "Producto B",
                    Precio = 6m,
                    Unidades = 2
                });

                // Guarda la instancia de factura y todas sus líneas
                db.Save(factura, true);

                // Carga la instacia de factura y todas sus líneas
                var facturaGuardada = db.LoadSingleById<Factura>(factura.Id);

                facturaGuardada.PrintDump();
                Console.ReadLine();
            }
        }
    }
}