using AutoMapper;
using Movies.API.Dtos;
using Movies.API.Models;

namespace Movies.API.MapperProfiles
{
    public class MovieEntertainerProfile : Profile
    {
        public MovieEntertainerProfile()
        {
            CreateMap<MovieEntertainer, MovieEntertainerReturnDto>();
            CreateMap<MovieEntertainerCreateDto, MovieEntertainer>();
        }
    }
}