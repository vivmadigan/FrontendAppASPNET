using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VentexiFrontend.Services;

namespace VentexiFrontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IInvoiceApiClient _api;
        public HomeController(IInvoiceApiClient api) => _api = api;

        // GET /Home/Index
        public async Task<IActionResult> Index()
        {
            // ← hard-code this just to see data:
            var userId = "user-456";
            var invoices = await _api.GetMyInvoicesAsync(userId);
            return View(invoices);
        }
    }
}
