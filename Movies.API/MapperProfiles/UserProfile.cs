using AutoMapper;
using Movies.API.Dtos;
using Movies.API.Models;

namespace Movies.API.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReturnDto>();
            CreateMap<UserCreateDto, User>();
        }
    }
}