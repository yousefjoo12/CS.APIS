using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<School, SchoolDTO>() 
                .ForMember(d => d.ImageCover, o => o.MapFrom<SchoolPictureUrlResolver>()).ReverseMap();


            CreateMap<School_Details, School_DetailsDTO>()
              .ForMember(d => d.School, o => o.MapFrom(s => s.School.SchoolName))
              .ForMember(d => d.ImageCover, o => o.MapFrom<School_DetailsPictureUrlResolver>())
              .ForMember(d => d.Images, o => o.MapFrom<School_DetailsPictureUrlResolver>()).ReverseMap();

        }
    }
}
