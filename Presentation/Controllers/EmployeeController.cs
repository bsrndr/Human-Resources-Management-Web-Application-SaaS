using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Models.VMs.Employee;
using Presentation.Models.VMs.Leave;
using System.Data;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Authorize(Roles = "CompanyManager, SiteManager, Employee")]
    public class EmployeeController : Controller
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

                var result = await client.GetFromJsonAsync<EmployeeVM>(serviceAddress + "Appuser/mainpage/" + id);

                return View(result);
            }
            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> EmployeeDetails(int id)
        {
            HttpClient client = new HttpClient();
            var result = await client.GetFromJsonAsync<EmployeeDetailsVM>(serviceAddress + "Appuser/detailspage/" + id);
            return View(result);
        }

        public async Task<IActionResult> UpdateEmployee(int id)
        {
            HttpClient client = new HttpClient();
            var result = await client.GetFromJsonAsync<UpdateEmployeeVM>(serviceAddress + "Appuser/updatepage/" + id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeVM model)
        {
            SendUpdateEmployeeVM sendUpdateVM = new SendUpdateEmployeeVM();
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
            var result = await client.PutAsJsonAsync<SendUpdateEmployeeVM>(serviceAddress + "Appuser/updateuser", sendUpdateVM);
            if (result.IsSuccessStatusCode)
            {
                TempData["info"] = "Update succeed";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["info"] = "Update failed";
                return View();
            }
        }


        public async Task<IActionResult> CreateLeaveRequest()
        {
            HttpClient client = new HttpClient();
            ClaimsPrincipal principle = HttpContext.User;
         
            int id = int.Parse(principle.FindFirst(ClaimTypes.PrimarySid)?.Value);
            ICollection<LeaveTypeVM> leaveTypes = await client.GetFromJsonAsync<List<LeaveTypeVM>>(serviceAddress + "Employee/getleavetypes/" + id);
          
            foreach (LeaveTypeVM leaveTypeVM in leaveTypes)
            {
                var list = leaveTypeVM.LeaveType.Split('L');
                leaveTypeVM.LeaveType = list[0] + " Leave";
               
            }
          
            ViewBag.LeaveType = new SelectList(leaveTypes, "Id", "LeaveType");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateLeaveRequest(CreateLeaveVM createLeaveVM)
        {
            int userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.PrimarySid)?.Value);
            createLeaveVM.UserId = userId;
            HttpClient client = new HttpClient();
            var result = await client.PostAsJsonAsync<CreateLeaveVM>(serviceAddress + "Employee/createleaverequest", createLeaveVM);
            if (result.IsSuccessStatusCode)
            {
                TempData["info"] = await result.Content.ReadAsStringAsync();
                return RedirectToAction("GetRequestList", new { id = userId });
            }
            else
            {
                TempData["info"] = await result.Content.ReadAsStringAsync();
                return RedirectToAction("CreateLeaveRequest");
            }

        }


        public async Task<IActionResult> GetRequestList(int id)
        {
            HttpClient client = new HttpClient();
            var result = await client.GetFromJsonAsync<List<LeaveAppUserVM>>(serviceAddress + "Employee/getleaverequests/" + id);
            if (!result.Any())
            {
                return View(result);
            }
            var list = result.OrderByDescending(result => result.Id).ToList();
            return View(list);
        }

        public async Task<IActionResult> DeleteRequest(int id)
        {
            HttpClient client = new HttpClient();
            ClaimsPrincipal principle = HttpContext.User;
            int userId = int.Parse(principle.FindFirst(ClaimTypes.PrimarySid)?.Value);
            var result = await client.DeleteAsync(serviceAddress + "Employee/deleteleave/" + id);
            return RedirectToAction("GetRequestList", new { id = userId });
        }
    }
}
