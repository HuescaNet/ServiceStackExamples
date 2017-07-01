namespace Conversores
{
    public class Color
    {
        public Color(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public static Color Rojo = new Color(255, 0, 0);
        public static Color Verde = new Color(0, 255, 0);
        public static Color Azul = new Color(0, 0, 255);
    }
}
