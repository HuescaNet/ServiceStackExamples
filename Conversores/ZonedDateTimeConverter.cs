using System;
using System.Data;
using NodaTime;
using ServiceStack.OrmLite;

namespace Conversores
{
    public class ZonedDateTimeConverter : OrmLiteConverter
    {
        public override string ColumnDefinition => "datetime2";

        public override DbType DbType => DbType.DateTime2;

        public override object ToDbValue(Type fieldType, object value)
        {
            return ((ZonedDateTime)value).ToDateTimeUtc();
        }

        public override object FromDbValue(Type fieldType, object value)
        {
            var timeZoneProvider = DateTimeZoneProviders.Tzdb;
            var idZonaHoraria = "Europe/Dublin";
            var zonaHorariaUsuario= timeZoneProvider[idZonaHoraria];
            var fecha = Instant.FromDateTimeUtc(DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc));
            return fecha.InZone(zonaHorariaUsuario);
        }
    }
}