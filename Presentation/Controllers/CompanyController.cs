using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Enums;
using Presentation.Models.VMs.Company;
using Presentation.Models.VMs.CompanyManager;
using Presentation.Models.VMs.Employee;

namespace Presentation.Controllers
{
    [Authorize(Roles ="CompanyManager, SiteManager")]
    public class CompanyController : Controller
    {
        public CompanyController(IMapper mapper)
        {
            this.mapper = mapper;
        }
        private readonly string serviceAddress = "https://hrtechwebapi.azurewebsites.net/api/";
        //private readonly string serviceAddress = "https://localhost:7169/api/";
        private IMapper mapper;

        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();

            string requestUrl = serviceAddress + "SiteManager/" + "getcompaniesactive";

            var result = await client.GetFromJsonAsync<List<CompanyVM>>(requestUrl);
            return View(result);
        }

        //View güncelle
        public async Task<IActionResult> CompanyDetails(int id)
        {
            HttpClient client = new HttpClient();
            GeneralDetailVm model = new GeneralDetailVm();
            var result = await client.GetFromJsonAsync<CompanyDetailsVM>(serviceAddress + "CompanyManager/getcompanydetails/" + id);
            var result1 = await client.GetFromJsonAsync<List<CompanyManagerVM>>(serviceAddress + "SiteManager/managersbycompany/" + id);
            var result2 = await client.GetFromJsonAsync<List<EmployeeVM>>(serviceAddress + "CompanyManager/getpersonelsbycompany/" + id);
            mapper.Map(result, model);
            model.CompanyManagers = result1;
            model.Employees = result2;

            return View(model);
        }

        public async Task<IActionResult> AddCompany()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany(CreateCompanyVM createCompanyVM)
        {
            Guid guid = Guid.NewGuid();
            string fileName = guid + createCompanyVM.Logo.FileName;

            FileStream fs = new FileStream("wwwroot/company-logos/" + fileName, FileMode.Create);
            await createCompanyVM.Logo.CopyToAsync(fs);
            createCompanyVM.ImagePath = fileName;

            if (createCompanyVM.ContratStartDate > DateTime.Now || createCompanyVM.ContratEndDate < DateTime.Now)
            {
                createCompanyVM.Status = Status.Passive;
            }
            else
            {
                createCompanyVM.Status = Status.Active;
            }

            HttpClient client = new HttpClient();
            var result = await client.PostAsJsonAsync<CreateCompanyVM>(serviceAddress + "SiteManager/" + "addcompany", createCompanyVM);
            if (result.IsSuccessStatusCode)
            {
                TempData["info"] = "Company has been created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["info"] = "An Error Occurred";
                return View();
            }
        }

        public async Task<IActionResult> GetCompanyId(string directory, int id)
        {
            HttpClient client = new HttpClient();
            var managerDetails = await client.GetFromJsonAsync<CompanyManagerDetailsVM>(serviceAddress + "AppUser/detailspage/" + id);
            if (directory == "details")
                return RedirectToAction("CompanyDetails", new { id = managerDetails.CompanyId });
            else
                return RedirectToAction("AddEmployee", "CompanyManager", new { id = managerDetails.CompanyId });
        }
    }
}
