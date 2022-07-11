using PdfCreator.Services.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfCreator.Services.Concreate
{
    public class ListToPdfService<T> : HtmlToPdfTemplateService
    {
        private readonly List<T> _list;

        public ListToPdfService(List<T> list)
        {
            _list = list;
        }

        public override string CreateBody()
        {
            var type = typeof(T);
            var sb = new StringBuilder();
            sb.Append($@"<body>
                           <div class='text-center'><h1>{typeof(T).Name} Table</h1></div>
                           <table class='table table-striped' align='center'>");


            type.GetProperties().ToList().ForEach(x =>
            {
                sb.Append($"<th>{x.Name}</th>");
            });

            _list.ForEach(x =>
            {
                var values = type.GetProperties().Select(properyInfo => properyInfo.GetValue(x, null)).ToList();

                sb.Append("<tr>");

                values.ForEach(value =>
                {
                    sb.Append($"<td class='p-3' align='center'>{value}</td>");
                });

                sb.Append("</tr>");
            });

            sb.Append("</table></body>");

            return sb.ToString();
        }
    }
}
