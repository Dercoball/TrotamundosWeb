using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;

public class WordGenerator
{
    public static byte[] GenerateWordDocument(Dictionary<string, string> placeholders, Dictionary<string, string> imagesBase64)
    {
        using (var memoryStream = new MemoryStream())
        {
            // Crear un nuevo documento de Word.
            using (var wordDocument = WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document, true))
            {
                // Agregar el contenido principal.
                var mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document(); // Crear el documento
                mainPart.Document.AppendChild(new Body()); // Agregar el cuerpo

                var body = mainPart.Document.Body;

                // Insertar texto y variables dinámicas.
                foreach (var placeholder in placeholders)
                {
                    var paragraph = new Paragraph(new Run(new Text($"{placeholder.Key}: {placeholder.Value}")));
                    body.AppendChild(paragraph);
                }

                // Insertar imágenes.
                foreach (var image in imagesBase64)
                {
                    var imageBytes = Convert.FromBase64String(image.Value);

                    // Agregar imagen al documento.
                    var imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);
                    using (var stream = new MemoryStream(imageBytes))
                    {
                        imagePart.FeedData(stream);
                    }

                    // Crear relación de imagen.
                    var imageId = mainPart.GetIdOfPart(imagePart);
                    var drawingElement = CreateImageElement(imageId);
                    var paragraph = new Paragraph(new Run(drawingElement));
                    body.AppendChild(paragraph);
                }

                mainPart.Document.Save(); // Guardar los cambios en el documento
            }

            return memoryStream.ToArray();
        }
    }

    private static Drawing CreateImageElement(string imageId)
    {
        // Crea un objeto de imagen para el documento.
        return new Drawing(
            new DocumentFormat.OpenXml.Drawing.Wordprocessing.Inline(
                new DocumentFormat.OpenXml.Drawing.Wordprocessing.Extent() { Cx = 990000L, Cy = 792000L },
                new DocumentFormat.OpenXml.Drawing.Wordprocessing.EffectExtent() { LeftEdge = 0L, TopEdge = 0L, RightEdge = 0L, BottomEdge = 0L },
                new DocumentFormat.OpenXml.Drawing.Wordprocessing.DocProperties() { Id = (UInt32Value)1U, Name = "Picture" },
                new DocumentFormat.OpenXml.Drawing.BlipFill(
                    new DocumentFormat.OpenXml.Drawing.Blip() { Embed = imageId },
                    new DocumentFormat.OpenXml.Drawing.Stretch(new DocumentFormat.OpenXml.Drawing.FillRectangle())
                ),
                new DocumentFormat.OpenXml.Drawing.ShapeProperties(
                    new DocumentFormat.OpenXml.Drawing.Transform2D(
                        new DocumentFormat.OpenXml.Drawing.Offset() { X = 0L, Y = 0L },
                        new DocumentFormat.OpenXml.Drawing.Extents() { Cx = 990000L, Cy = 792000L }
                    ),
                    new DocumentFormat.OpenXml.Drawing.PresetGeometry(new DocumentFormat.OpenXml.Drawing.AdjustValueList()) { Preset = DocumentFormat.OpenXml.Drawing.ShapeTypeValues.Rectangle }
                )
            )
        );
    }
}
