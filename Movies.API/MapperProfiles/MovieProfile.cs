using AutoMapper;
using Movies.API.Dtos;
using Movies.API.Models;

namespace Movies.API.MapperProfiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieReturnDto>();
            CreateMap<MovieCreateDto, Movie>();
        }
    }
}