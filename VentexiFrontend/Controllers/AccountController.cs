using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace VentexiFrontend.Controllers
{
    public class AccountController : Controller
    {
        // GET /Account/Login?role=User   (or ?role=Admin)
        [HttpGet]
        public async Task<IActionResult> Login(string role = "User")
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, role + "Tester"),
                new Claim(ClaimTypes.Role, role)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );

            if (role == "Admin")
                return RedirectToAction("Index", "AdminInvoices");
            else
                return RedirectToAction("Index", "Invoices");
        }

        // POST /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
