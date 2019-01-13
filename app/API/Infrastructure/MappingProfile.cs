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
                .ForMember(user => user.Login, opt => opt.MapFrom(user => user.Login.Trim()))
                .ForMember(user => user.Roles, opt => opt.NullSubstitute("Guest"))
                .ForMember(user => user.TodoList, opt => opt.MapFrom(user => user.TodoList.Trim()));
            CreateMap<DTO.SignupModel, Model.User>()
                .ForMember(user => user.Login, opt => opt.MapFrom(user => user.Login.Trim()))
                .ForMember(user => user.Email, opt => opt.MapFrom(user => user.Email.Trim()))
                .ForMember(user => user.TodoList, opt => opt.MapFrom(user => user.TodoList.Trim()))
                .ForMember(user => user.Password, opt => opt.MapFrom(user => user.Password.Trim()))
                .ForMember(user => user.Roles, opt => opt.MapFrom(user => user.Roles == null ? "Guest" : user.Roles.Trim()));

            CreateMap<Model.Company, DTO.Company>();
            CreateMap<DTO.Company, Model.Company>()
                .ForMember(company => company.Id, opt => opt.Ignore())
                .ForMember(company => company.Status, opt => opt.NullSubstitute(Model.Constants.Status.DRAFT));

            CreateMap<Model.Zoning, DTO.Zoning>()
                .ForMember(zoning => zoning.Surface, opt => opt.ConvertUsing(new DoubleSurfaceConverter()));
            CreateMap<DTO.Zoning, Model.Zoning>()
                .ForMember(zoning => zoning.Id, opt => opt.Ignore())
                .ForMember(zoning => zoning.Surface, opt => opt.ConvertUsing(new StringSurfaceConverter()));

            CreateMap<Model.ActivitySector, DTO.ActivitySector>();
            CreateMap<DTO.ActivitySector, Model.ActivitySector>()
                .ForMember(sector => sector.Id, opt => opt.Ignore());

            CreateMap<Model.Coordinates, DTO.Coordinates>()
                .ForMember(coordinates => coordinates.Latitude, opt => opt.ConvertUsing(new DoubleCoordinatesConverter()))
                .ForMember(coordinates => coordinates.Longitude, opt => opt.ConvertUsing(new DoubleCoordinatesConverter()));
            CreateMap<DTO.Coordinates, Model.Coordinates>()
                .ForMember(coordinates => coordinates.Id, opt => opt.Ignore())
                .ForMember(coordinates => coordinates.Latitude, opt => opt.ConvertUsing(new StringCoordinatesConverter()))
                .ForMember(coordinates => coordinates.Longitude, opt => opt.ConvertUsing(new StringCoordinatesConverter()));
        }
    }
}
