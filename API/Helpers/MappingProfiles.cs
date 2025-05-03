using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Students, StudentsDTO>()
                //.ForMember(d => d.Faculty, o => o.MapFrom(s => s.Faculty.Fac_Name))
                //.ForMember(d => d.FacultyYearSemister, o => o.MapFrom(s => s.FacultyYearSemister.Sem_Name))
                .ForMember(d => d.St_Image, o => o.MapFrom<StudentsPictureUrlResolver>());


        }
    }
}
