using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using VentexiFrontend.Services;

using VentexiFrontend.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Cookie?based authentication
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";   // stub login page
        options.LogoutPath = "/Account/Logout";
    });

// 2. We’ll need IHttpContextAccessor to see the user’s role in our factory
builder.Services.AddHttpContextAccessor();




builder.Services
    .AddHttpClient("UserClient", client =>
    {
        client.BaseAddress = new Uri("https://ventixe-invoice-microservice-group3.azurewebsites.net/api/");
        client.DefaultRequestHeaders.Add("x-api-key", "1a76c263-4d83-4c98-b913-9029f9dfad7d");
    });

builder.Services
    .AddHttpClient("AdminClient", client =>
    {
        client.BaseAddress = new Uri("https://ventixe-invoice-microservice-group3.azurewebsites.net/api/");
        client.DefaultRequestHeaders.Add("x-api-key", "fba16aa0-4bb4-4bb7-9201-d81937292329");
    });

builder.Services.AddScoped<IInvoiceApiClient>(sp =>
{
    var httpCtx = sp.GetRequiredService<IHttpContextAccessor>().HttpContext!;
    var clientName = httpCtx.User.IsInRole("Admin")
        ? "AdminClient"
        : "UserClient";

    var factory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = factory.CreateClient(clientName);

    return new InvoiceApiClient(httpClient);
});


builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

// *order matters*
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
