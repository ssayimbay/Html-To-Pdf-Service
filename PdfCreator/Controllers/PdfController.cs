using Microsoft.AspNetCore.Mvc;
using PdfCreator.Models;
using PdfCreator.Services.Concreate;
using System.Collections.Generic;
using System.IO;

namespace PdfCreator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateList(List<Product> products)
        {
            var listToPdfService = new ListToPdfService<Product>(products);
            return File(listToPdfService.CreatePdf().ToArray(), "application/pdf", "sea.pdf");
        }

    }
}
