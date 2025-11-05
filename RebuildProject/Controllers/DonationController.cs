using BusinceLayer.Interfaces;
using BusinceLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using static BusinceLayer.Services.DontationDto;

namespace RebuildProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly IBaseService<Donation, DonationDto, CreateDonationDto, DonationDto> _donationService;

        public DonationController(IBaseService<Donation, DonationDto, CreateDonationDto, DonationDto> donationService)
        {
            _donationService = donationService;
        }

        // GET: api/Donation
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var donations = await _donationService.GetAllAsync();
            return Ok(donations);
        }

        // GET: api/Donation/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var donation = await _donationService.GetByIdAsync(id);
            if (donation == null)
                return NotFound($"Donation with ID {id} not found.");

            return Ok(donation);
        }

        // POST: api/Donation
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDonationDto createDonationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdDonation = await _donationService.AddAsync(createDonationDto);
            return CreatedAtAction(nameof(GetById), new { id = createdDonation.DonationsId }, createdDonation);
        }

        // PUT: api/Donation/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DonationDto updateDonationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _donationService.UpdateAsync(id, updateDonationDto);
            if (!result)
                return NotFound($"Donation with ID {id} not found.");

            return NoContent();
        }

        // DELETE: api/Donation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _donationService.DeleteAsync(id);
            if (!deleted)
                return NotFound($"Donation with ID {id} not found.");

            return NoContent();
        }
    }
}
