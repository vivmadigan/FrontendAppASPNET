using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using VentexiFrontend.Services;

using VentexiFrontend.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddHttpClient<IInvoiceApiClient, InvoiceApiClient>(client =>
    {
        client.BaseAddress = new Uri("https://ventixe-invoice-microservice-group3.azurewebsites.net/api/");
        client.DefaultRequestHeaders.Add("x-api-key", "1a76c263-4d83-4c98-b913-9029f9dfad7d");
    });

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

// (no authentication/authorization middleware for this smoke-test)

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
app.Run();
