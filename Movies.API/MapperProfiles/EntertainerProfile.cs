using AutoMapper;
using Movies.API.Dtos;
using Movies.API.Models;

namespace Movies.API.MapperProfiles
{
    public class EntertainerProfile : Profile
    {
        public EntertainerProfile()
        {
            CreateMap<Entertainer, EntertainerReturnDto>();
            CreateMap<EntertainerCreateDto, Entertainer>();
        }
    }
}