using AutoMapper;
using DTO;
using Model;

namespace API.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Model.User, DTO.User>().ReverseMap();
            CreateMap<Model.Company, DTO.Company>().ReverseMap();
            CreateMap<Model.ActivitySector, DTO.ActivitySector>().ReverseMap();
        }
    }
}