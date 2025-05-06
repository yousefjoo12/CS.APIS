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
               // .ForMember(d => d.FacultyYearSemister, o => o.MapFrom(s => s.FacultyYearSemister.Sem_Name))
                .ForMember(d => d.St_Image, o => o.MapFrom<StudentsPictureUrlResolver>());

            //CreateMap<FacultyYear, FacultyYearDTO>()
            // .ForMember(d => d.FacultyYear, o => o.MapFrom(s => s.Faculty.Fac_Name));


            CreateMap<Studets_Rooms, Studets_RoomsDTO>()
             .ForMember(d => d.Rooms, o => o.MapFrom(s => s.Rooms.Room_Num))
             .ForMember(d => d.Students, o => o.MapFrom(s => s.Students.St_NameAr));

            CreateMap<Subjects, SubjectsDTO>()
             .ForMember(d => d.Doctors, o => o.MapFrom(s => s.Doctors.Dr_NameAr))
             .ForMember(d => d.Instructors, o => o.MapFrom(s => s.Instructors.Ins_NameAr))
             .ForMember(d => d.FacultyYearSemister, o => o.MapFrom(s => s.FacultyYearSemister.Sem_Name));




        }
    }
}
