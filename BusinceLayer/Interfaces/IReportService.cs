using BusinceLayer.EntitiesDTOS;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Interfaces
{
    public interface IReportService : IBaseService<Report, ReportDto, CreateReportDto, UpdateReportDto>
    {
        // دوال إضافية لو بدك
        // مثلاً البحث عن بلاغ مع الصور
        //Task<ReportDto> AddReportWithImagesAsync(CreateReportDto dto);
    }
}
