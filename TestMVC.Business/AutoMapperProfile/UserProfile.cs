using AutoMapper;
using TestMVC.Model.DTO.User;
using TestMVC.Model.User;

namespace TestMVC.Business.AutoMapperProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, UserViewModel>()
                .ForMember(dest =>
                    dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest =>
                    dest.Email,
                    opt => opt.MapFrom(src => src.Email))
                .ForMember(dest =>
                    dest.Username,
                    opt => opt.MapFrom(src => src.Username))
                .ForMember(dest =>
                    dest.Genre,
                    opt => opt.MapFrom(src => src.Genre.Equals('M') ? "Masculino" : "Femenino"))
                .ForMember(dest =>
                    dest.Status,
                    opt => opt.MapFrom(src => src.Status ? "ACTIVE" : "INACTIVE"))
                .ForMember(dest =>
                    dest.DateCreated,
                    opt => opt.MapFrom(src => src.DateCreated));
        }
    }
}
