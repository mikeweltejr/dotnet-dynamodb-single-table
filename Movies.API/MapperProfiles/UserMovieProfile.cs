using AutoMapper;
using Movies.API.Dtos;
using Movies.API.Models;

namespace Movies.API.MapperProfiles
{
    public class UserMovieProfile : Profile
    {
        public UserMovieProfile()
        {
            CreateMap<UserMovie, UserMovieReturnDto>();
            CreateMap<UserMovieCreateDto, UserMovie>();
        }
    }
}