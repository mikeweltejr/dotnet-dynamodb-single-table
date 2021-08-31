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
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MoviesController(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var movies = await _movieRepository.Get();
            var moviesReturnDto = _mapper.Map<List<MovieReturnDto>>(movies);

            return Ok(moviesReturnDto);
        }

        [HttpGet("{id}", Name = "MovieLink")]
        public async Task<IActionResult> Get(string id)
        {
            var movie = await _movieRepository.Get(id);

            if(movie is null) return new NotFoundObjectResult($"Movie with id: {id} not found");

            var movieReturnDto = _mapper.Map<MovieReturnDto>(movie);

            return Ok(movieReturnDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MovieCreateDto movieCreateDto)
        {
            var movie = _mapper.Map<Movie>(movieCreateDto);

            await _movieRepository.Save(movie);

            var movieReturnDto = _mapper.Map<MovieReturnDto>(movie);

            return CreatedAtRoute(
                routeName: "MovieLink",
                routeValues: new { id=movie.Id },
                value: movieReturnDto
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var movie = await _movieRepository.Get(id);

            if(movie is null) return new NotFoundObjectResult($"Movie with id: {id} not found");

            await _movieRepository.Delete(movie);

            return NoContent();
        }
    }
}