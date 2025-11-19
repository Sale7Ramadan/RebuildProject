using AutoMapper;
using BusinceLayer.EntitiesDTOS;
using BusinceLayer.Interfaces;
using BusinceLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RebuildProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IBaseService<Report, ReportDto, CreateReportDto, UpdateReportDto> _reportService;
        private readonly IMapper _mapper;
        private readonly IReportService reportService1;
        public ReportController(IBaseService<Report, ReportDto, CreateReportDto, UpdateReportDto> reportService
            ,IMapper mapper,IReportService reportservice)
        {
            _reportService = reportService;
            _mapper = mapper;    
            reportService1 = reportservice;
        }

        // GET: api/Report
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var result = await _reportService.GetAllWithIncludeAsync(
        x => x.City,
        x => x.User,
        x => x.Category
    );
            return Ok(result);
        }

        // GET: api/Report/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var report = await _reportService.GetByIdAsync(id);
            if (report == null)
                return NotFound($"Report with ID {id} not found.");

            return Ok(report);
        }

        // POST: api/Report
        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] CreateReportDto createReportDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
            
        //    var createdReport = await _reportService.AddAsync(createReportDto);
        //    return CreatedAtAction(nameof(GetById), new { id = createdReport.ReportId }, createdReport);
        //}
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReportDto createReportDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

         
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
                return Unauthorized();

          
            var report = _mapper.Map<Report>(createReportDto);

          
            report.UserId = userId;

          
            var createdReport = await reportService1.AddReportWithUserAsync(report, createReportDto.ImagesBase64);
           
            return CreatedAtAction(nameof(GetById), new { id = createdReport.ReportId }, createdReport);
        }
        // PUT: api/Report/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateReportDto updateReportDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _reportService.UpdateAsync(id, updateReportDto);
            if (!result)
                return NotFound($"Report with ID {id} not found.");

            return NoContent();
        }

        // DELETE: api/Report/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _reportService.DeleteAsync(id);
            if (!deleted)
                return NotFound($"Report with ID {id} not found.");

            return NoContent();
        }
    }
}
