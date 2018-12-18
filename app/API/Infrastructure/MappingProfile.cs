using AutoMapper;
using DTO;
using Model;

namespace API.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateMap<SOURCE, DESTIONATION>();

            CreateMap<Model.User, DTO.User>();
            CreateMap<DTO.User, Model.User>()
                .ForMember(user => user.Id, opt => opt.Ignore())
                .ForMember(user => user.Password, opt => opt.Ignore())
                .ForMember(user => user.Roles, opt => opt.NullSubstitute(Model.Constants.Roles.USER));
            CreateMap<DTO.SigninModel, Model.User>()
                .ForMember(user => user.Roles, opt => opt.NullSubstitute(Model.Constants.Roles.USER));

            CreateMap<Model.Company, DTO.Company>();
            CreateMap<DTO.Company, Model.Company>()
                .ForMember(company => company.Id, opt => opt.Ignore())
                .ForMember(company => company.IdOpenData, opt => opt.Ignore());
            
            CreateMap<Model.ActivitySector, DTO.ActivitySector>();
            CreateMap<DTO.ActivitySector, Model.ActivitySector>()
                .ForMember(sector => sector.Id, opt => opt.Ignore());

            //CreateMap<Model.User, DTO.User>().ReverseMap();
        }
    }
}
