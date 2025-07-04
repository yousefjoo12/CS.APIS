﻿using API.DTOs;
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
            CreateMap<Students, StudentsDTO>()
                .ForMember(d => d.FacultyYearSemister, o => o.MapFrom(s => s.FacultyYearSemister.Sem_Name))  
                .ForMember(d => d.St_Image, o => o.MapFrom<StudentsPictureUrlResolver>()).ReverseMap();

            CreateMap<Doctors, DoctorsDTO>()
                .ForMember(d => d.Faculty, o => o.MapFrom(s => s.Faculty.Fac_Name)) 
                .ForMember(d => d.Dr_Image, o => o.MapFrom<DoctorsPictureUrlResolver>()).ReverseMap();

            CreateMap<Notification, NotificationDTO>()
                  .ForMember(d => d.FacultyYearSemister, o => o.MapFrom(s => s.FacultyYearSemister.Sem_Name)).ReverseMap();

            CreateMap<Attendance_T, AttendanceDTO>()
                 .ForMember(d => d.Lecture, o => o.MapFrom(s => s.Lecture.Lecture_Name)).ReverseMap();

            CreateMap<FacultyYear, FacultyYearDTO>()
                .ForMember(d => d.Faculty, o => o.MapFrom(s => s.Faculty.Fac_Name)).ReverseMap();

            CreateMap<FacultyYearSemister, FacultyYearSemisterDTO>()
                 .ForMember(d => d.FacultyYear, o => o.MapFrom(s => s.FacultyYear.Year)).ReverseMap();

           
            CreateMap<Subjects, SubjectsDTO>()
             .ForMember(d => d.Doctors, o => o.MapFrom(s => s.Doctors.Dr_NameAr))
             .ForMember(d => d.FacultyYearSemister, o => o.MapFrom(s => s.FacultyYearSemister.Sem_Name)).ReverseMap(); 
            CreateMap<Lecture_S, LectureDTO>() 
             .ForMember(d => d.Subjects, o => o.MapFrom(s => s.Subjects.Sub_Name)).ReverseMap(); 
             
        }
    }
}
