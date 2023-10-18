using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Models.Enums;
using Presentation.Models.VMs.Company;
using Presentation.Models.VMs.CompanyManager;
using Presentation.Models.VMs.Departments;
using Presentation.Models.VMs.SiteManager;
using System.Data;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Authorize(Roles = "SiteManager")]
    public class SiteManagerController : Controller
    {
        private readonly string serviceAddress = "https://hrtechwebapi.azurewebsites.net/api/";
        //private readonly string serviceAddress = "https://localhost:7169/api/";

        public async Task<IActionResult> Index()
        {
            ClaimsPrincipal principle = HttpContext.User;

            int id = int.Parse(principle.FindFirst(ClaimTypes.PrimarySid)?.Value);

            if (id != 0)
            {
                HttpClient client = new HttpClient();

                var result = await client.GetFromJsonAsync<UserVM>(serviceAddress + "AppUser/mainpage/" + id);
                return View(result);
            }
            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> Details(int id)
        {
            HttpClient client = new HttpClient();
            var result = await client.GetFromJsonAsync<DetailsVM>(serviceAddress + "AppUser/detailspage/" + id);
            return View(result);
        }

        public async Task<IActionResult> Update(int id)
        {
            HttpClient client = new HttpClient();
            var result = await client.GetFromJsonAsync<UpdateVM>(serviceAddress + "AppUser/updatepage/" + id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateVM updateVM)
        {
            SendUpdateVM sendUpdateVM = new SendUpdateVM();
            if (updateVM.PostedPath != null)
            {
                Guid guid = Guid.NewGuid();
                string fileName = guid + updateVM.PostedPath.FileName;

                FileStream fs = new FileStream("wwwroot/user-images/" + fileName, FileMode.Create);
                await updateVM.PostedPath.CopyToAsync(fs);
                updateVM.ImagePath = fileName;
                sendUpdateVM.Id = updateVM.Id;
                sendUpdateVM.Address = updateVM.Address;
                sendUpdateVM.PhoneNumber = updateVM.PhoneNumber;
                sendUpdateVM.ImagePath = fileName;
            }
            else
            {
                sendUpdateVM.Id = updateVM.Id;
                sendUpdateVM.Address = updateVM.Address;
                sendUpdateVM.PhoneNumber = updateVM.PhoneNumber;
                sendUpdateVM.ImagePath = "none";
            }
            HttpClient client = new HttpClient();
            var result = await client.PutAsJsonAsync<SendUpdateVM>(serviceAddress + "AppUser/updateuser", sendUpdateVM);
            if (result.IsSuccessStatusCode)
            {
                TempData["info"] = "User has been successfully updated";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["info"] = "An error occurred";
                return View();
            }
        }

        public async Task<IActionResult> AddCompanyManager()
        {
            HttpClient client = new HttpClient();
            ICollection<CompanyVM> companies = await client.GetFromJsonAsync<List<CompanyVM>>(serviceAddress + "SiteManager/getcompaniesactive/");
            ICollection<DepartmentVM> departments = await client.GetFromJsonAsync<List<DepartmentVM>>(serviceAddress + "AppUser/getdepartments");

            SelectList companiesCombo = new SelectList(companies, "Id", "CompanyName");
            SelectList departmentsCombo = new SelectList(departments, "Id", "DepartmentName");

            ViewBag.Companies = companiesCombo;
            ViewBag.Departments = departmentsCombo;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCompanyManager(CreateCompanyManagerVM model)
        {
            Guid guid = Guid.NewGuid();
            string fileName = guid + model.PostedPath.FileName;

            FileStream fs = new FileStream("wwwroot/user-images/" + fileName, FileMode.Create);
            await model.PostedPath.CopyToAsync(fs);
            model.ImagePath = fileName;

            if (model.DateOfRecruitment > DateTime.Now)
                model.Status = Status.Passive;
            else
                model.Status = Status.Active;

            HttpClient client = new HttpClient();
            var result = await client.PostAsJsonAsync<CreateCompanyManagerVM>(serviceAddress + "SiteManager/addcompanymanager", model);
            if (result.IsSuccessStatusCode)
            {
                TempData["info"] = "User has been created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["info"] = "User has not been created";
                return View();
            }
        }
    }
}
