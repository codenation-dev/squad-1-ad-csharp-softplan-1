using AutoMapper;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.Domain.Models;

namespace CentralDeErros.Application.Mapping
{
    public class AutoMappingDomainToViewModel : Profile
    {
        public AutoMappingDomainToViewModel()
        {
            CreateMap<ErrorLog, ErrorLogViewModel>();
            CreateMap<User, UserViewModel>();
        }
    }
}
