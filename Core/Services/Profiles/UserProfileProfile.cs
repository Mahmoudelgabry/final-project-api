using AutoMapper;
using Domain.Models;
using Shared.DTOs.UserProfile;

namespace Shared.Mapping
{
    public class UserProfileProfile : Profile
    {
        public UserProfileProfile()
        {
            CreateMap<UserProfile, GetUserProfileDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.User.Email));

            CreateMap<UpdateUserProfileDto, UserProfile>();
        }
    }
}