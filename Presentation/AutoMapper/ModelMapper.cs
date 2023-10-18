using AutoMapper;
using Presentation.Models.VMs.Company;
using Presentation.Models.VMs.CompanyManager;
using Presentation.Models.VMs.Departments;

namespace Presentation.AutoMapper
{
    internal class ModelMapper : Profile
    {
        public ModelMapper()
        {
            CreateMap<GeneralDetailVm, CompanyDetailsVM>().ReverseMap();
        }
    }
}
