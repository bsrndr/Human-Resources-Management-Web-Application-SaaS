using Microsoft.AspNetCore.Mvc;
using Presentation.Models.ModelMetaDatas;

namespace Presentation.Models.VMs.SiteManager
{

    public class SendUpdateVM
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? ImagePath { get; set; }
    }
}
