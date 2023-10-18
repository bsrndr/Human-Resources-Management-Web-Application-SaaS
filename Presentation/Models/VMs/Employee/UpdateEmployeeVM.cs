using Microsoft.AspNetCore.Mvc;
using Presentation.Models.ModelMetaDatas;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.VMs.Employee
{
    [ModelMetadataType(typeof(UpdateCompanyManagerVmMetaData))]
    public class UpdateEmployeeVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? SecondFirstName { get; set; }
        public string LastName { get; set; }
        public string? SecondLastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        public IFormFile PostedPath { get; set; }
    }
}
