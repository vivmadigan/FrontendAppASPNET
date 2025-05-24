using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VentexiFrontend.Services;
using VentexiFrontend.ViewModels;

namespace VentexiFrontend.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminInvoicesController : Controller
    {
        private readonly IInvoiceApiClient _api;
        public AdminInvoicesController(IInvoiceApiClient api) => _api = api;

        public async Task<IActionResult> Index(string? selectedId)
        {
            var invoices = (await _api.GetAllInvoicesAsync()).ToList();

            var vm = new InvoicesPageViewModel
            {
                Invoices = invoices,
                SelectedInvoice = invoices
                                      .FirstOrDefault(i => i.InvoiceId == selectedId)
                                  ?? invoices.FirstOrDefault()
            };
            return View(vm);
        }
        public async Task<IActionResult> Download(string id)
        {
            var pdf = await _api.AdminDownloadInvoicePdfAsync(id);
            return File(
                pdf,
                contentType: "application/pdf",
                fileDownloadName: $"invoice_{id}.pdf"
            );
        }
    }
}
