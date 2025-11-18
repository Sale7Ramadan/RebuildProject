using AutoMapper;
using BusinceLayer.EntitiesDTOS;
using BusinceLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Services
{
    public class ReportService : BaseService<Report, ReportDto, CreateReportDto, UpdateReportDto>, IReportService
    {
        public ReportService(IBaseRepositories<Report> repo, IMapper mapper) : base(repo, mapper) { }

        public override async Task<ReportDto> AddAsync(CreateReportDto dto)
        {
            var report = _mapper.Map<Report>(dto);
            await _repository.AddAsync(report);

            report.ReportImages = dto.ImagesBase64.Select(b64 => new ReportImage
            {
                ImageUrl = SaveBase64ToFile(b64),
                ReportId = report.ReportId
            }).ToList();

            await _repository.UpdateAsync(report); // حفظ الصور بعد إضافة البلاغ

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
    }

}
