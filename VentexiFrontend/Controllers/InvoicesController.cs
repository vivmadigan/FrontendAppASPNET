using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VentexiFrontend.Services;
using VentexiFrontend.Models;
using VentexiFrontend.ViewModels;


namespace VentexiFrontend.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IInvoiceApiClient _api;
        public InvoicesController(IInvoiceApiClient api) => _api = api;

        // GET /Invoices?selectedId=INV10012
        public async Task<IActionResult> Index(string? selectedId)
        {
            var userId = "user-456";
            var invoices = (await _api.GetMyInvoicesAsync(userId)).ToList();

            var vm = new InvoicesPageViewModel
            {
                Invoices = invoices,
                SelectedInvoice = invoices
                                      .FirstOrDefault(i => i.InvoiceId == selectedId)
                                  ?? invoices.FirstOrDefault()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay(string id)
        {
            var userId = "user-456";
            await _api.PayInvoiceAsync(userId, id);
            return RedirectToAction("Index", new { selectedId = id });
        }

    }
}