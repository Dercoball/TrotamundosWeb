namespace TrotamundosNetCore.Clases.Util
{
    public class ColorGrafica
    {
        public int id { get; set; }
        public int r { get; set; }
        public int g { get; set; }
        public int b { get; set; }
        public decimal a { get; set; }

        public ColorGrafica()
        {

        }

        public override string ToString()
        {
            return $"rgba({r},{g},{b},{a})";
        }

        public ColorGrafica Clone()
        {
            return new ColorGrafica()
            {
                r = r,
                g = g,
                b = b,
                a = a
            };
        }
    }
}
