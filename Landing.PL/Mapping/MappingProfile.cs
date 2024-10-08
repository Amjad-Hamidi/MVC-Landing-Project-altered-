using AutoMapper;
using Landing.DAL.Models;
using Landing.PL.Areas.Dashboard.ViewModels;

namespace Landing.PL.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile() 
		{
			CreateMap<ServiceFormVM, Service>().ReverseMap(); // from VM to M and from M to VM
			CreateMap<Service, ServicesVM>(); // from M to VM
			CreateMap<Service, ServiceDetailsVM>(); // from M to VM
		}
	}
}
