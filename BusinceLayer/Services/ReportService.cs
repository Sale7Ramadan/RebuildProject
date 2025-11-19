using AutoMapper;
using BusinceLayer.EntitiesDTOS;
using BusinceLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Services
{
    public class ReportService : BaseService<Report, ReportDto, CreateReportDto, UpdateReportDto>, IReportService
    {
        private readonly IReportRepository _reportRepository;

       
        public ReportService(IReportRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
            _reportRepository = repository;
        }

        
        public async Task<ReportDto> AddReportWithUserAsync(Report report, List<string> imagesBase64)
        {
            report.ReportImages = imagesBase64.Select(b64 => new ReportImage
            {
                ImageUrl = SaveBase64ToFile(b64)
            }).ToList();

            await _repository.AddAsync(report);
            return _mapper.Map<ReportDto>(report);
        }

        private string SaveBase64ToFile(string base64)
        {
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            var bytes = Convert.FromBase64String(base64);
            var fileName = $"{Guid.NewGuid()}.jpg";
            var filePath = Path.Combine(folder, fileName);
            File.WriteAllBytes(filePath, bytes);
            return $"/images/{fileName}";
        }

        

       
        public async Task<bool> IncrementLikesAsync(int reportId)
        {
            return await _reportRepository.IncrementLikesAsync(reportId);
        }

       
        public async Task<IEnumerable<ReportDto>> GetMostLikedReportsAsync(int count)
        {
            var reports = await _reportRepository.GetMostLikedReportsAsync(count);
            return _mapper.Map<IEnumerable<ReportDto>>(reports);
        }

        
        public async Task<IEnumerable<ReportDto>> GetReportsByCityAsync(int cityId)
        {
            var reports = await _reportRepository.GetReportsByCityAsync(cityId);
            return _mapper.Map<IEnumerable<ReportDto>>(reports);
        }
    }
}