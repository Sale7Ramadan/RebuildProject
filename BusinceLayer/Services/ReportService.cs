//using AutoMapper;
//using BusinceLayer.EntitiesDTOS;
//using BusinceLayer.Interfaces;
//using DataAccessLayer.Entities;
//using DataAccessLayer.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BusinceLayer.Services
//{
//    public class ReportService
//    : BaseService<Report, ReportDto, CreateReportDto, UpdateReportDto>, IReportService
//    {
//        public ReportService(
//            IBaseRepositories<Report> repo,
//            IMapper mapper,
//            IFileService fileService)
//            : base(repo, mapper)
//        {
//            _fileService = fileService;
//        }

//        public override async Task<ReportDto> AddAsync(CreateReportDto dto)
//        {
//            var report = _mapper.Map<Report>(dto);

//            await _repository.AddAsync(report);

//            foreach (var img in dto.Images)
//            {
//                var path = await _fileService.Save(img);

//                await _reportImageRepo.AddAsync(new ReportImage
//                {
//                    ImagePath = path,
//                    ReportId = report.ReportId
//                });
//            }

//            return _mapper.Map<ReportDto>(report);
//        }
//    }
//}