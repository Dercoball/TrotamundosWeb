namespace TrotamundosNetCore.Clases
{
    public class RequestModel
    {
        public Dictionary<string, string> Placeholders { get; set; } // Texto dinámico
        public Dictionary<string, string> ImagesBase64 { get; set; } // Imágenes en Base64
    }

}
