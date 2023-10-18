using Microsoft.AspNetCore.Mvc;
using Presentation.Models.ModelMetaDatas;

namespace Presentation.Models.VMs.Login
{
    [ModelMetadataType(typeof(ConfirmationNumberVmMetaData))]
    public class ConformationNumberVM
    {
        public int Id { get; set; }
        public int ConformationNumber { get; set; }
        public string Password { get; set; }

    }
}
