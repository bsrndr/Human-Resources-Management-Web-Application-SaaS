using Microsoft.AspNetCore.Mvc;
using Presentation.Models.ModelMetaDatas;

namespace Presentation.Models.VMs.Password
{
    [ModelMetadataType(typeof(ChangePasswordVmMetaData))]
    public class ChangePasswordVM
    {
        public int Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
