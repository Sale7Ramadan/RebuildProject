using AutoMapper;
using BusinceLayer.EntitiesDTOS;
using BusinceLayer.Services;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinceLayer.Services.DontationDto;

namespace BusinceLayer.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {





            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();




            CreateMap<City, CityDto>();
            CreateMap<CreateCityDto, City>();
            CreateMap<UpdateCityDto, City>();

            CreateMap<Comment, CommentDto>()

          .ForMember(dest => dest.UserName, opt =>
          opt.MapFrom(src => src.User.FirstName.ToString()
          + " " + src.User.LastName.ToString()));

            CreateMap<CreateCommentDto, Comment>();
            CreateMap<UpdateCommentDto, Comment>();







            CreateMap<Donation, DonationDto>()
                .ForMember(dest => dest.DonationCaseTitle, opt => opt.MapFrom(src => src.DonationCase.Report.Title))
                .ForMember(dest => dest.DonorDisplayName, opt => opt.MapFrom(src =>
                    src.User != null ? src.User.FirstName.ToString() +
                    "" + src.User.LastName.ToString() : src.DonorName));

            CreateMap<CreateDonationDto, Donation>();




            CreateMap<DonationCase, DonationCaseDto>()
                .ForMember(dest => dest.ReportTitle, opt => opt.MapFrom(src => src.Report.Title))
                .ForMember(dest => dest.ReportDescription, opt => opt.MapFrom(src => src.Report.Description));

            CreateMap<CreateDonationCaseDto, DonationCase>();
            CreateMap<UpdateDonationCaseDto, DonationCase>();






            CreateMap<Report, ReportDto>()
    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
    .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.CityName))
    .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Comments.Count))
    .ForMember(dest => dest.ReportImages, opt => opt.MapFrom(src => src.ReportImages))
    .ForMember(dest => dest.TotalCollectedAmount, opt => opt.MapFrom(src => src.DonationCases.Sum(dc => dc.CollectedAmount ?? 0)))
    .ForMember(dest => dest.HasDonationCase, opt => opt.MapFrom(src => src.DonationCases.Any()));

            CreateMap<CreateReportDto, Report>();
           
     


            CreateMap<UpdateReportDto, Report>();

            // --- ReportImage Mappings ---
            CreateMap<ReportImage, ReportImageDto>();
            CreateMap<CreateReportImageDto, ReportImage>();

            CreateMap<ReportImageDto, ReportImage>();


            CreateMap<User, UserDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.ReportsCount, opt => opt.MapFrom(src => src.Reports.Count))
                .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count))
                .ForMember(dest => dest.DonationsCount, opt => opt.MapFrom(src => src.Donations.Count));

            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.PassHash, opt => opt.Ignore()); 

          
            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.PassHash, opt => opt.Ignore());


            


        }
        }
}
