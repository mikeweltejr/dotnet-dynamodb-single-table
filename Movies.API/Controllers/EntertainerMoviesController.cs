using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Dtos;
using Movies.API.Repositories;

namespace Movies.API.Controllers
{
    [ApiController]
    [Route("api/entertainers/{entertainerId}/movies")]
    public class EntertainerMoviesController : ControllerBase
    {
        private readonly IEntertainerRepository _entertainerRepository;
        private readonly IEntertainerMovieRepository _entertainerMovieRepository;
        private readonly IMapper _mapper;

        public EntertainerMoviesController(IEntertainerRepository entertainerRepository,
                                            IEntertainerMovieRepository entertainerMovieRepository,
                                            IMapper mapper
        )
        {
            _entertainerRepository = entertainerRepository;
            _entertainerMovieRepository = entertainerMovieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string entertainerId)
        {
            var entertainer = await _entertainerRepository.Get(entertainerId);
            if(entertainer is null) return new NotFoundObjectResult($"Entertainer with id: {entertainerId} not found");

            var movieEntertainers = await _entertainerMovieRepository.Get(entertainerId);
            var movieEntertainersReturnDto = _mapper.Map<List<MovieEntertainerReturnDto>>(movieEntertainers);

            return Ok(movieEntertainersReturnDto);
        }
    }
}