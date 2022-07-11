using DinkToPdf;
using System.IO;
using System.Text;

namespace PdfCreator.Services.Abstract
{
    public abstract class HtmlToPdfTemplateService
    {
        private readonly StringBuilder _stringBuilder;

        public HtmlToPdfTemplateService()
        {
            _stringBuilder = new StringBuilder();
        }

        public virtual string CreateHeader()
        {
            return _stringBuilder.Append("<head>")
                                 .Append("</head>")
                                 .ToString();
        }

        public virtual string CreateBody()
        {
            return _stringBuilder.Append("<body>")
                                 .Append("</body>")
                                 .ToString();
        }


        public MemoryStream CreatePdf()
        {
            var header = CreateHeader();
            var body = CreateBody();
            var htmlDocument = _stringBuilder.Append("<html>")
                                             .Append(header)
                                             .Append(body)
                                             .Append("</html>")
                                             .ToString();


            var pdfBytes = GetPdfBytes(htmlDocument);
            return new(pdfBytes);
        }

        private byte[] GetPdfBytes(string htmlDocument)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    PageOffset = 5,
                    Margins =
                    {
                       Bottom = 5
                    },
                 },
                Objects =
                {
                    new ObjectSettings()
                    {
                        HtmlContent = htmlDocument,

                        WebSettings =
                        {
                            EnableIntelligentShrinking = true,
                            EnableJavascript = true,
                            DefaultEncoding = "utf-8",
                            UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/css/bootstrap.css")
                        },
                        HeaderSettings =
                        {
                            FontSize = 15,
                            Spacing = 5,
                        },
                     }
                }
            };

            var converter = new SynchronizedConverter(new PdfTools());
            return converter.Convert(doc);
        }
    }
}
