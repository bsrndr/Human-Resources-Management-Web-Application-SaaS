using Microsoft.AspNetCore.Mvc;
using Presentation.Models.ModelMetaDatas;

namespace Presentation.Models.VMs.SiteManager
{
    [ModelMetadataType(typeof(UpdateVmMetaData))]
    public class UpdateVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? SecondFirstName { get; set; }
        public string LastName { get; set; }
        public string? SecondLastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        public IFormFile? PostedPath { get; set; }
    }
}
