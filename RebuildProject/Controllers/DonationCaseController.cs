using BusinceLayer.Services;
using BusinceLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace RebuildProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationCaseController : ControllerBase
    {
        private readonly IBaseService<DonationCase, DonationCaseDto, CreateDonationCaseDto, UpdateDonationCaseDto> _donationCaseService;

        public DonationCaseController(IBaseService<DonationCase, DonationCaseDto, CreateDonationCaseDto, UpdateDonationCaseDto> donationCaseService)
        {
            _donationCaseService = donationCaseService;
        }

        // GET: api/DonationCase
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var donationCases = await _donationCaseService.GetAllAsync();
            return Ok(donationCases);
        }

        // GET: api/DonationCase/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var donationCase = await _donationCaseService.GetByIdAsync(id);
            if (donationCase == null)
                return NotFound($"DonationCase with ID {id} not found.");

            return Ok(donationCase);
        }

        // POST: api/DonationCase
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDonationCaseDto createDonationCaseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdDonationCase = await _donationCaseService.AddAsync(createDonationCaseDto);
            return CreatedAtAction(nameof(GetById), new { id = createdDonationCase.DonationCaseId }, createdDonationCase);
        }

        // PUT: api/DonationCase/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDonationCaseDto updateDonationCaseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _donationCaseService.UpdateAsync(id, updateDonationCaseDto);
            if (!result)
                return NotFound($"DonationCase with ID {id} not found.");

            return NoContent();
        }

        // DELETE: api/DonationCase/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _donationCaseService.DeleteAsync(id);
            if (!deleted)
                return NotFound($"DonationCase with ID {id} not found.");

            return NoContent();
        }
    }
}
