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
        Task<ReportDto> AddReportWithUserAsync(Report report,List<string> imagesBase64);
    }
}
