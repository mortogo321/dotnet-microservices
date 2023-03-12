using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.Dtos;
using PlatformService.Data.Repos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/platforms")]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public PlatformController(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlatforms()
        {
            try
            {
                var platforms = await _repository.GetAllPlatformsAsync();

                return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllPlatforms: {0}", ex);

                return StatusCode(500, "GetAllPlatforms: Internal server error");
            }
        }

        [HttpGet]
        [Route("{id}", Name = "GetPlatformById")]
        public async Task<IActionResult> GetPlatformById(int id)
        {
            var platform = await _repository.GetPlatformByIdAsync(id);

            if (platform != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platform));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlatform(PlatformCreateDto platform)
        {
            var platformModal = _mapper.Map<PlatformCreateDto, Platform>(platform);

            await _repository.CreatePlatformAsync(platformModal);
            await _repository.SaveChangesAsync();

            var result = _mapper.Map<PlatformReadDto>(platformModal);

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = result.Id }, result);
        }
    }
}