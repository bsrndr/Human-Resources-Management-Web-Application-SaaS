using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Models.Enums;
using Presentation.Models.VMs.Company;
using Presentation.Models.VMs.CompanyManager;
using Presentation.Models.VMs.Departments;
using Presentation.Models.VMs.Employee;
using Presentation.Models.VMs.Leave;
using System.Data;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Authorize(Roles = "CompanyManager, SiteManager")]
    public class CompanyManagerController : Controller
    {
        private readonly string serviceAddress = "https://hrtechwebapi.azurewebsites.net/api/";
        //private readonly string serviceAddress = "https://localhost:7169/api/";
        public async Task<IActionResult> Index()
        {
            ClaimsPrincipal principle = HttpContext.User;
            if (!principle.Claims.Any())
            {
                return RedirectToAction("Index", "Login");
            }
            int id = int.Parse(principle.FindFirst(ClaimTypes.PrimarySid)?.Value);

            if (id != 0)
            {
                HttpClient client = new HttpClient();

                var result = await client.GetFromJsonAsync<CompanyManagerVM>(serviceAddress + "Appuser/mainpage/" + id);

                return View(result);
            }
            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> CompanyManagerDetails(int id)
        {
            HttpClient client = new HttpClient();
            var result = await client.GetFromJsonAsync<CompanyManagerDetailsVM>(serviceAddress + "Appuser/detailspage/" + id);
            return View(result);
        }

        public async Task<IActionResult> UpdateCompanyManager(int id)
        {
            HttpClient client = new HttpClient();
            var result = await client.GetFromJsonAsync<UpdateCompanyManagerVM>(serviceAddress + "Appuser/updatepage/" + id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCompanyManager(UpdateCompanyManagerVM model)
        {
            SendUpdateCompanyManagerVM sendUpdateVM = new SendUpdateCompanyManagerVM();
            if (model.PostedPath != null)
            {

                Guid guid = Guid.NewGuid();
                string fileName = guid + model.PostedPath.FileName;

                FileStream fs = new FileStream("wwwroot/user-images/" + fileName, FileMode.Create);
                await model.PostedPath.CopyToAsync(fs);
                model.ImagePath = fileName;
                sendUpdateVM.Id = model.Id;
                sendUpdateVM.Address = model.Address;
                sendUpdateVM.PhoneNumber = model.PhoneNumber;
                sendUpdateVM.ImagePath = fileName;

            }
            else
            {
                sendUpdateVM.Id = model.Id;
                sendUpdateVM.Address = model.Address;
                sendUpdateVM.PhoneNumber = model.PhoneNumber;
                sendUpdateVM.ImagePath = "none";
            }

            HttpClient client = new HttpClient();
            var result = await client.PutAsJsonAsync<SendUpdateCompanyManagerVM>(serviceAddress + "Appuser/updateuser", sendUpdateVM); // + update api, sendUpdateVM
            if (result.IsSuccessStatusCode)
            {
                TempData["info"] = "User has been successfully updated";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["info"] = "User has not been created. An error occurred";
                return View();
            }
        }

        public async Task<IActionResult> AddEmployee(int id)
        {
            CreateEmployeeVM model = new CreateEmployeeVM();
            HttpClient client = new HttpClient();
            ICollection<DepartmentVM> departments = await client.GetFromJsonAsync<List<DepartmentVM>>(serviceAddress + "Appuser/getdepartments");

            SelectList departmentsCombo = new SelectList(departments, "Id", "DepartmentName");

            ViewBag.Departments = departmentsCombo;

            model.CompanyId = id;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(CreateEmployeeVM model)
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
            var result = await client.PostAsJsonAsync<CreateEmployeeVM>(serviceAddress + "CompanyManager/addemployee", model);
            if (result.IsSuccessStatusCode)
            {
                TempData["info"] = "User has been created successfully";
                return RedirectToAction("CompanyDetails", "Company", new { id = model.CompanyId });
            }
            else
            {
                TempData["info"] = "User has not been created. An error occured. Something may be coused this like email or TC doublication!";
                return RedirectToAction("AddEmployee");
            }
            
        }
        
        /// <summary>
        /// fetch ile listelenecek
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetEmployeeLeaves(int id)
        {
            return View(id);
        }

    }
}
