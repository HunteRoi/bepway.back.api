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
                .ForMember(user => user.Roles, opt => opt.NullSubstitute("Guest"));
            CreateMap<DTO.SignupModel, Model.User>()
                .ForMember(user => user.Roles, opt => opt.NullSubstitute("Guest"));

            CreateMap<Model.Company, DTO.Company>();
            CreateMap<DTO.Company, Model.Company>()
                .ForMember(company => company.Id, opt => opt.Ignore())
                .ForMember(company => company.IdOpenData, opt => opt.Ignore());

            CreateMap<Model.Zoning, DTO.Zoning>()
                .ForMember(z => z.Surface, opt => opt.ConvertUsing(new DoubleConverter()));
            CreateMap<DTO.Zoning, Model.Zoning>()
                .ForMember(z => z.Surface, opt => opt.ConvertUsing(new StringDoubleConverter()))
                .ForMember(zoning => zoning.Id, opt => opt.Ignore())
                .ForMember(zoning => zoning.IdOpenData, opt => opt.Ignore());

            CreateMap<Model.ActivitySector, DTO.ActivitySector>();
            CreateMap<DTO.ActivitySector, Model.ActivitySector>()
                .ForMember(sector => sector.Id, opt => opt.Ignore());

            CreateMap<Model.Coordinates, DTO.Coordinates>()
                .ForMember(c => c.Latitude, opt => opt.ConvertUsing(new DoubleConverter()))
                .ForMember(c => c.Longitude, opt => opt.ConvertUsing(new DoubleConverter()));
            CreateMap<DTO.Coordinates, Model.Coordinates>()
                .ForMember(c => c.Latitude, opt => opt.ConvertUsing(new StringDoubleConverter()))
                .ForMember(c => c.Longitude, opt => opt.ConvertUsing(new StringDoubleConverter()));
        }
    }
}