namespace TrotamundosNetCore.Clases.Util
{
    public class ArchivoTelegram
    {
        public string nombre { get; set; }
        public byte[] data { get; set; }
        public string mensaje { get; set; }
        public int sistema { get; set; }
        public int subsistema { get; set; }

        public ArchivoTelegram()
        {

        }

        public ArchivoTelegram(string nombre, byte[] data, string mensaje, int sistema, int subsistema)
        {
            this.nombre = nombre;
            this.data = data;
            this.mensaje = mensaje;
            this.sistema = sistema;
            this.subsistema = subsistema;
        }
    }
}
