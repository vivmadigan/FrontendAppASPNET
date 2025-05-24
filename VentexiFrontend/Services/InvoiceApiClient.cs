using System.Text.Json;
using VentexiFrontend.Models;

namespace VentexiFrontend.Services
{
    public interface IInvoiceApiClient
    {
        Task<IEnumerable<InvoiceModel>> GetMyInvoicesAsync(string userId);
        Task<InvoiceModel> PayInvoiceAsync(string userId, string invoiceId);
        Task<IEnumerable<InvoiceModel>> GetAllInvoicesAsync();
    }

    public class InvoiceApiClient : IInvoiceApiClient
    {
        private readonly HttpClient _http;
        public InvoiceApiClient(HttpClient http) => _http = http;

        public async Task<IEnumerable<InvoiceModel>> GetMyInvoicesAsync(string userId)
        {
            // build the GET /api/Invoices/user-invoices
            using var req = new HttpRequestMessage(HttpMethod.Get, "Invoices/user-invoices");

            // add the required x-user-id header
            req.Headers.Add("x-user-id", userId);

            using var res = await _http.SendAsync(req);
            if (!res.IsSuccessStatusCode)
            {
                var text = await res.Content.ReadAsStringAsync();
                throw new Exception($"Invoice API failed ({res.StatusCode}): {text}");
            }

            // deserialize the JSON payload
            var invoices = await res.Content.ReadFromJsonAsync<IEnumerable<InvoiceModel>>();
            return invoices ?? Enumerable.Empty<InvoiceModel>();
        }
        public async Task<InvoiceModel> PayInvoiceAsync(string userId, string invoiceId)
        {
            using var req = new HttpRequestMessage(
                HttpMethod.Put,
                $"Invoices/user-pay-invoice/{invoiceId}"
            );
            req.Headers.Add("x-user-id", userId);

            using var res = await _http.SendAsync(req);
            res.EnsureSuccessStatusCode();

            // returns the updated invoice (your API returns Ok(result.Invoice))
            return await res.Content.ReadFromJsonAsync<InvoiceModel>()
                   ?? throw new Exception("Empty response from PayInvoice");
        }
        public async Task<IEnumerable<InvoiceModel>> GetAllInvoicesAsync()
        {
            // GET /api/Invoices/admin-get-all-invoices
            using var req = new HttpRequestMessage(HttpMethod.Get, "Invoices/admin-get-all-invoices");
            using var res = await _http.SendAsync(req);
            if (!res.IsSuccessStatusCode)
            {
                var text = await res.Content.ReadAsStringAsync();
                throw new Exception($"Invoice API failed ({res.StatusCode}): {text}");
            }
            var invoices = await res.Content.ReadFromJsonAsync<IEnumerable<InvoiceModel>>();
            return invoices ?? Enumerable.Empty<InvoiceModel>();
        }
    }
}
