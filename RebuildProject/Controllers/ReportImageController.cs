using BusinceLayer.EntitiesDTOS;
using BusinceLayer.Interfaces;
using BusinceLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace RebuildProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportImageController : ControllerBase
    {
        private readonly IBaseService<ReportImage, ReportImageDto, CreateReportImageDto, ReportImageDto> _reportImageService;

        public ReportImageController(IBaseService<ReportImage, ReportImageDto, CreateReportImageDto, ReportImageDto> reportImageService)
        {
            _reportImageService = reportImageService;
        }

        // GET: api/ReportImage
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var images = await _reportImageService.GetAllAsync();
            return Ok(images);
        }

        // GET: api/ReportImage/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var image = await _reportImageService.GetByIdAsync(id);
            if (image == null)
                return NotFound($"Image with ID {id} not found.");

            return Ok(image);
        }

        // POST: api/ReportImage
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReportImageDto createReportImageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdImage = await _reportImageService.AddAsync(createReportImageDto);
            return CreatedAtAction(nameof(GetById), new { id = createdImage.ImageId }, createdImage);
        }

        // DELETE: api/ReportImage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _reportImageService.DeleteAsync(id);
            if (!deleted)
                return NotFound($"Image with ID {id} not found.");

            return NoContent();
        }
    }
}
