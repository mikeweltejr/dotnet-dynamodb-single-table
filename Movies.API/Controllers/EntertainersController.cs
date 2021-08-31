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
    public class EntertainersController : ControllerBase
    {
        private readonly IEntertainerRepository _entertainerRepository;
        private readonly IMapper _mapper;

        public EntertainersController(IEntertainerRepository entertainerRepository, IMapper mapper)
        {
            _entertainerRepository = entertainerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var entertainers = await _entertainerRepository.Get();
            var entertainersReturnDto = _mapper.Map<List<EntertainerReturnDto>>(entertainers);

            return Ok(entertainersReturnDto);
        }

        [HttpGet("{id}", Name = "EntertainerLink")]
        public async Task<IActionResult> Get(string id)
        {
            var entertainer = await _entertainerRepository.Get(id);

            if(entertainer is null) return new NotFoundObjectResult($"Entertainer with id: {id} not found");

            var entertainerReturnDto = _mapper.Map<EntertainerReturnDto>(entertainer);

            return Ok(entertainerReturnDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EntertainerCreateDto entertainerCreateDto)
        {
            var entertainer = _mapper.Map<Entertainer>(entertainerCreateDto);

            await _entertainerRepository.Save(entertainer);

            var entertainerReturnDto = _mapper.Map<EntertainerReturnDto>(entertainer);

            return CreatedAtRoute(
                routeName: "EntertainerLink",
                routeValues: new { id=entertainer.Id },
                value: entertainerReturnDto
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var entertainer = await _entertainerRepository.Get(id);

            if(entertainer is null) return new NotFoundObjectResult($"Entertainer with id: {id} not found");

            await _entertainerRepository.Delete(entertainer);

            return NoContent();
        }
    }
}