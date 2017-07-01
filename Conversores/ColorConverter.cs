using System;
using System.Data;
using ServiceStack.OrmLite;

namespace Conversores
{
    public class ColorConverter : OrmLiteConverter
    {
        private string tipo;

        public ColorConverter(string tipo)
        {
            this.tipo = tipo;
        }

        public override string ColumnDefinition => tipo;

        public override DbType DbType => DbType.Int32;

        public override object ToDbValue(Type fieldType, object value)
        {
            var color = value as Color;
            return color.R << 16 | color.G << 8 | color.B;
        }

        public override object FromDbValue(Type fieldType, object value)
        {
            return new Color((byte)(((int)value & 0xFF0000) >> 16), (byte)(((int)value & 0x00FF00) >> 8), (byte)((int)value & 0x0000FF));
        }
    }
}
