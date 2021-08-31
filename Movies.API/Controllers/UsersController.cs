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
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userRepository.Get();
            var usersReturnDto = _mapper.Map<List<UserReturnDto>>(users);
            return Ok(usersReturnDto);
        }

        [HttpGet("{id}", Name = "UserLink")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _userRepository.Get(id);

            if(user is null) return new NotFoundObjectResult($"User with id: {id} not found");

            var userReturnDto = _mapper.Map<UserReturnDto>(user);
            return Ok(userReturnDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserCreateDto userCreateDto)
        {
            var user = _mapper.Map<User>(userCreateDto);
            
            await _userRepository.Save(user);

            var userReturnDto = _mapper.Map<UserReturnDto>(user);

            return CreatedAtRoute(
                routeName: "UserLink",
                routeValues: new { id=user.Id },
                value: userReturnDto
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userRepository.Get(id);

            if(user is null ) return new NotFoundObjectResult($"User with id: {id} not found");

            await _userRepository.Delete(user);

            return NoContent();
        }
    }
}