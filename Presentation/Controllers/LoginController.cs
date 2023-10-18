using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.VMs.Login;
using System.Data;
using System.Net;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly string serviceAddress = "https://hrtechwebapi.azurewebsites.net/api/SıgnIn/";
        //private readonly string serviceAddress = "https://localhost:7169/api/SıgnIn/";

        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            HttpClient client = new HttpClient();

            var result = await client.PostAsJsonAsync(serviceAddress + "login", loginVM);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                ClaimUserVM claimUserVm = await result.Content.ReadFromJsonAsync<ClaimUserVM>();

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, claimUserVm.UserName),
                    new Claim(ClaimTypes.Role, claimUserVm.Role),
                    new Claim(ClaimTypes.PrimarySid, claimUserVm.Id.ToString()),
                    new Claim(ClaimTypes.Thumbprint, claimUserVm.ImagePath)
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                AuthenticationProperties authenticationProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(1)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

                if (claimUserVm.Role == "SiteManager")
                    return RedirectToAction("Index", "SiteManager");
                else if (claimUserVm.Role == "CompanyManager")
                    return RedirectToAction("Index", "CompanyManager");
                else
                    return RedirectToAction("Index", "Employee");
            }
            else
            {
                TempData["Error"] = "Incorrect username or password.";
                return RedirectToAction("Index", "Login");
                //
            }
        }

        [HttpGet("resetpassword/{id}")]
        public async Task<IActionResult> ResetPassword(int id)
        {
            ConformationNumberVM conformationNumberVM = new ConformationNumberVM();
            conformationNumberVM.Id = id;
            return View(conformationNumberVM);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordPost(ConformationNumberVM model)
        {
            HttpClient client = new HttpClient();
            var result = await client.PutAsJsonAsync(serviceAddress + "resetpassword", model);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            TempData["error"] = "Your password cannot be the same as last three password or Conformation number is wrong";
            return RedirectToAction("ResetPassword", new { id = model.Id });
        }

    }
}
