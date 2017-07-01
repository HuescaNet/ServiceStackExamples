using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ServiceStack.Logging;
using ServiceStack.OrmLite;
using ServiceStack.Text;

namespace Consultas
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

            // Factoría para crear instancias de conexiones a base de datos configurada 
            // para utilizar el dialecto de SQLite y funcionar en memoria
            //var dbFactory = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider);

            using (var db = dbFactory.Open())
            {
                // Elimina las tablas de la base de datos
                db.DropTable<LineaFactura>();
                db.DropTable<Factura>();
                db.DropTable<Producto>();

                // Crea las tablas a partir de la definición de los POCOs
                db.CreateTable<Producto>();
                db.CreateTable<Factura>();
                db.CreateTable<LineaFactura>();

                GenerarDatos(db);

                // Configura el sistema de log para mostrar las SQL generadas
                LogManager.LogFactory = new ConsoleLogFactory();

                // 10 primeros productos y total de unidades vendidas en 
                // los últimos 12 meses
                var query = db.From<LineaFactura>()
                    .Join<Producto>()
                    .Join<Factura>()
                    .Where<Factura>(f => f.Fecha >= DateTime.Today.AddMonths(-12))
                    .GroupBy<Producto>(p => p.Nombre)
                    .OrderByDescending<LineaFactura>(l => Sql.Sum(l.Unidades))
                    .Take(10)
                    .Select<Producto, LineaFactura>((p, l) => new
                    {
                        p.Nombre,
                        Unidades = Sql.Sum(l.Unidades)
                    });

                OrmLiteConfig.ThrowOnError = true;

                // Proyección a POCO
                var resultadoPoco = db.Select<ResultadoConsulta>(query);
                resultadoPoco.PrintDump();

                // Proyección a tupla
                var resultadoTupla = db.Select<(string nombre, int unidades)>(query);
                resultadoTupla.PrintDump();
                
                // Proyección a dynamic
                var resultadoDynamic = db.Select<dynamic>(query);
                resultadoDynamic.PrintDump();

                // Proyección a diccionario
                var resultadoDiccionario = db.Dictionary<string, int>(query);
                resultadoDiccionario.PrintDump();
            }
            Console.ReadLine();
        }

        // Genera datos de prueba
        private static void GenerarDatos(IDbConnection db)
        {
            var random = new Random();

            var productos = new List<Producto>();
            for (int i = 0; i < 100; i++)
            {
                productos.Add(new Producto { Nombre = $"Producto {i}" });
            }
            db.SaveAll(productos);

            var facturas = new List<Factura>();
            for (int i = 0; i < 100; i++)
            {
                facturas.Add(new Factura
                {
                    Numero = $"Factura {i}",
                    Fecha = DateTime.Today.AddDays(-random.Next(1000))
                });
            }
            db.SaveAll(facturas);

            var lineas = new List<LineaFactura>();
            foreach (var factura in facturas)
            {
                for (int i = 0; i < 10; i++)
                {
                    lineas.Add(new LineaFactura
                    {
                        FacturaId = factura.Id,
                        Precio = (decimal)random.NextDouble() * 100m,
                        Unidades = random.Next(1, 10),
                        ProductoId = productos.Skip(random.Next(100)).First().Id
                    });
                }
            }
            db.SaveAll(lineas);
        }
    }

    class ResultadoConsulta
    {
        public string Nombre { get; set; }
        public int Unidades { get; set; }
    }
}