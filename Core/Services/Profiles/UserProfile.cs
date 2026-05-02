using AutoMapper;
using Domain.Models;
using Shared;

namespace Services.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(d => d.FullName,
                    opt => opt.MapFrom(s => s.FirstName + " " + s.LastName));

            CreateMap<RegisterDto, User>();
            CreateMap<User, UserProfileDto>();
        }
    }
}