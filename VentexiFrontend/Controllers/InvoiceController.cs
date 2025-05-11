using Microsoft.AspNetCore.Mvc;

namespace VentexiFrontend.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
