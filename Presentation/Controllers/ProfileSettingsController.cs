using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.VMs.Password;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Authorize(Roles = "CompanyManager, SiteManager, Employee")]
    public class ProfileSettingsController : Controller
    {
        private readonly string serviceAddress = "https://hrtechwebapi.azurewebsites.net/api/SıgnIn/";
        //private readonly string serviceAddress = "https://localhost:7169/api/SıgnIn/";
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            ClaimsPrincipal principle = HttpContext.User;
            int id = int.Parse(principle.FindFirst(ClaimTypes.PrimarySid)?.Value);

            HttpClient client = new HttpClient();

            var response = await client.PutAsJsonAsync<ChangePasswordVM>(serviceAddress + "changepassword/" + id, model);

            if (response.IsSuccessStatusCode)
            {
                TempData["info"] = "Password has been updated successfully";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                TempData["ErrorMessage"] = "Something went wrong";
                return View("Index", model);
            }

        }
    }
}
