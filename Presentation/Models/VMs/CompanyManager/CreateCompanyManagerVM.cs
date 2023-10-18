using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Enums;
using Presentation.Models.ModelMetaDatas;

namespace Presentation.Models.VMs.CompanyManager
{
    [ModelMetadataType(typeof(CreateCompanyManagerVmMetaData))]
    public class CreateCompanyManagerVM
    {
        public string FirstName { get; set; }
        public string? SecondFirstName { get; set; }
        public string LastName { get; set; }
        public string? SecondLastName { get; set; }
        public string Tc { get; set; }
        public decimal Salary { get; set; }
        public DateTime BirthDate { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime DateOfRecruitment { get; set; }
        public string Title { get; set; }
        public int DepartmentId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public IFormFile PostedPath { get; set; }

        public Status Status { get; set; }

        public string Gender { get; set; }

        public int CompanyId { get; set; }
    }
}
