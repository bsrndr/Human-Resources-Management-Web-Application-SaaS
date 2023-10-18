using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Presentation.Controllers
{
    [Authorize(Roles = "CompanyManager, SiteManager, Employee")]
    public class LogoutController : Controller
    {     
        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index","Login");
        }
    }
}
