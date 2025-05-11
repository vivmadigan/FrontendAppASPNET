using System.Text.Json;
using VentexiFrontend.Models;

namespace VentexiFrontend.Services
{
    public interface IInvoiceApiClient
    {
        Task<IEnumerable<InvoiceModel>> GetMyInvoicesAsync(string userId);
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
    }
}
