using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Dtos;
using Movies.API.Models;
using Movies.API.Repositories;

namespace Movies.API.Controllers
{
    [ApiController]
    [Route("api/movies/{movieId}/entertainers")]
    public class MovieEntertainersController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IEntertainerRepository _entertainerRepository;
        private readonly IMovieEntertainerRepository _movieEntertainerRepository;
        private readonly IMapper _mapper;

        public MovieEntertainersController(IMovieRepository movieRepository,
                                            IEntertainerRepository entertainerRepository,
                                            IMovieEntertainerRepository movieEntertainerRepository,
                                            IMapper mapper
        )
        {
            _movieRepository = movieRepository;
            _entertainerRepository = entertainerRepository;
            _movieEntertainerRepository = movieEntertainerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string movieId)
        {
            var movie = await _movieRepository.Get(movieId);
            if(movie is null) return new NotFoundObjectResult($"Movie with id: {movieId} not found");

            var movieEntertainers = await _movieEntertainerRepository.Get(movieId);
            var movieEntertainersReturnDto = _mapper.Map<List<MovieEntertainerReturnDto>>(movieEntertainers);

            return Ok(movieEntertainersReturnDto);
        }

        [HttpGet("{entertainerId}", Name = "MovieEntertainerLink")]
        public async Task<IActionResult> Get(string movieId, string entertainerId)
        {
            var movie = await _movieRepository.Get(movieId);
            if(movie is null) return new NotFoundObjectResult($"Movie with id: {movieId} not found");

            var entertainer = await _entertainerRepository.Get(entertainerId);
            if(entertainer is null) return new NotFoundObjectResult($"Entertainer with id: {entertainerId} not found");

            var movieEntertainer = await _movieEntertainerRepository.Get(movieId, entertainerId);
            if(movieEntertainer is null) return new NotFoundObjectResult($"Movie with entertainerId: {entertainerId} not found");

            var movieEntertainerReturnDto = _mapper.Map<MovieEntertainerReturnDto>(movieEntertainer);

            return Ok(movieEntertainerReturnDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string movieId, [FromBody] MovieEntertainerCreateDto movieEntertainerCreateDto)
        {
            var movie = await _movieRepository.Get(movieId);
            if(movie is null) return new NotFoundObjectResult($"Movie with id: {movieId} not found");

            var existingMovieEntertainer = await _movieEntertainerRepository.Get(movieId, movieEntertainerCreateDto.EntertainerId);
            if(existingMovieEntertainer is not null) 
                return new ConflictObjectResult($"Movie with entertainerId: {movieEntertainerCreateDto.EntertainerId} not found");

            var movieEntertainer = _mapper.Map<MovieEntertainer>(movieEntertainerCreateDto);
            movieEntertainer.Movieid = movieId;

            await _movieEntertainerRepository.Save(movieEntertainer);

            var movieEntertainerReturnDto = _mapper.Map<MovieEntertainerReturnDto>(movieEntertainer);

            return CreatedAtRoute(
                routeName: "MovieEntertainerLink",
                routeValues: new { movieId=movieId, entertainerId=movieEntertainerCreateDto.EntertainerId},
                value: movieEntertainerReturnDto
            );
        }

        [HttpDelete("{entertainerId}")]
        public async Task<IActionResult> Delete(string movieId, string entertainerId)
        {
            var movie = await _movieRepository.Get(movieId);
            if(movie is null) return new NotFoundObjectResult($"Movie with id: {movieId} not found");

            var entertainer = await _entertainerRepository.Get(entertainerId);
            if(entertainer is null) return new NotFoundObjectResult($"Entertainer with id: {entertainerId} not found");

            var movieEntertainer = await _movieEntertainerRepository.Get(movieId, entertainerId);
            if(movieEntertainer is null) return new NotFoundObjectResult($"Movie with entertainerId: {entertainerId} not found");          

            await _movieEntertainerRepository.Delete(movieEntertainer);

            return NoContent(); 
        }
    }
}