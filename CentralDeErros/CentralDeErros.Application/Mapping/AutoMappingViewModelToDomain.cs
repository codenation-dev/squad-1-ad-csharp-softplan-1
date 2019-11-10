using AutoMapper;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.Domain.Models;

namespace CentralDeErros.Application.Mapping
{
    public class AutoMappingViewModelToDomain : Profile
    {
        public AutoMappingViewModelToDomain()
        {
            CreateMap<ErrorLogViewModel, ErrorLog>();
        }
    }
}
