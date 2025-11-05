using BusinceLayer.Services;
using BusinceLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace RebuildProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IBaseService<City, CityDto, CreateCityDto, UpdateCityDto> _cityService;

        public CityController(IBaseService<City, CityDto, CreateCityDto, UpdateCityDto> cityService)
        {
            _cityService = cityService;
        }

        // GET: api/City
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cities = await _cityService.GetAllAsync();
            return Ok(cities);
        }

        // GET: api/City/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var city = await _cityService.GetByIdAsync(id);
            if (city == null)
                return NotFound($"City with ID {id} not found.");

            return Ok(city);
        }

        // POST: api/City
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCityDto createCityDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdCity = await _cityService.AddAsync(createCityDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCity.CityId }, createdCity);
        }

        // PUT: api/City/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCityDto updateCityDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _cityService.UpdateAsync(id, updateCityDto);
            if (!result)
                return NotFound($"City with ID {id} not found.");

            return NoContent();
        }

        // DELETE: api/City/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _cityService.DeleteAsync(id);
            if (!deleted)
                return NotFound($"City with ID {id} not found.");

            return NoContent();
        }
    }
}
