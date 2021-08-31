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
    [Route("api/users/{userId}/movies")]
    public class UserMoviesController : ControllerBase
    {
        private readonly IUserMovieRepository _userMovieRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public UserMoviesController(IUserMovieRepository userMovieRepository, 
                                    IUserRepository userRepository,
                                    IMovieRepository movieRepository,
                                    IMapper mapper)
        {
            _userMovieRepository = userMovieRepository;
            _userRepository = userRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string userId)
        {
            var user = await _userRepository.Get(userId);
            
            if(user is null) return new NotFoundObjectResult($"User with id: {userId} not found");

            var userMovies  = await _userMovieRepository.Get(userId);
            var userMoviesReturnDto = _mapper.Map<List<UserMovieReturnDto>>(userMovies);

            return Ok(userMoviesReturnDto);
        }

        [HttpGet("{movieId}", Name = "UserMovieLink")]
        public async Task<IActionResult> Get(string userId, string movieId)
        {
            var user = await _userRepository.Get(userId);
            if(user is null) return new NotFoundObjectResult($"User with id: {userId} not found");

            var movie = await _movieRepository.Get(movieId);
            if(movie is null) return new NotFoundObjectResult($"Movie with id: {movieId} not found");

            var userMovie = await _userMovieRepository.Get(userId, movieId);
            if(userMovie is null) return new NotFoundObjectResult($"User does not have movie with id: {movieId} added");

            var userMovieReturnDto = _mapper.Map<UserMovieReturnDto>(userMovie);

            return Ok(userMovieReturnDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string userId, [FromBody] UserMovieCreateDto userMovieCreateDto)
        {
            var user = await _userRepository.Get(userId);
            if(user is null) return new NotFoundObjectResult($"User with id: {userId} not found");

            var existingUserMovie = await _userMovieRepository.Get(userId, userMovieCreateDto.MovieId);
            if(existingUserMovie is not null) return new ConflictObjectResult($"User with movie id: {userMovieCreateDto.MovieId} already exists");

            var userMovie = _mapper.Map<UserMovie>(userMovieCreateDto);
            userMovie.UserId = userId;

            await _userMovieRepository.Save(userMovie);

            var userMovieReturnDto = _mapper.Map<UserMovieReturnDto>(userMovie);

            return CreatedAtRoute(
                routeName: "UserMovieLink",
                routeValues: new { userId=userId, movieId=userMovie.MovieId},
                value: userMovieReturnDto
            );
        }

        [HttpDelete("{movieId}")]
        public async Task<IActionResult> Delete(string userId, string movieId)
        {
            var user = await _userRepository.Get(userId);
            if(user is null) return new NotFoundObjectResult($"User with id: {userId} not found");

            var movie = await _movieRepository.Get(movieId);
            if(movie is null) return new NotFoundObjectResult($"Movie with id: {movieId} not found");

            var userMovie = await _userMovieRepository.Get(userId, movieId);
            if(userMovie is null) return new NotFoundObjectResult($"User does not have movie with id: {movieId} added");

            await _userMovieRepository.Delete(userMovie);

            return NoContent();
        }
    }
}