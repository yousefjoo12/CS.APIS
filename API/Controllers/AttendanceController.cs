using API.DTOs;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Repositories.Contract;
using Core.Specifications.SubjectsSpecifications;
using Core.Specifications.SubjectsSpecParamsSpecifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.APIS.Erorrs;
using Repository.Data;

namespace API.Controllers
{
    public class AttendanceController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly StoreContext _context;

        public AttendanceController(IUnitOfWork unitOfWork, IMapper mapper, StoreContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        } 
        [HttpGet("GetAllSubjects")]   //Subjects
        public async Task<ActionResult<IReadOnlyList<AttendanceDTO>>> GetAllAttendances()
        {
            var result = await _context.Students
     .Join(_context.Studets_Subject,
         student => student.ID,
         ss => ss.St_ID,
         (student, ss) => new { student, ss })
     .Join(_context.Subjects,
         s => s.ss.Sub_ID,
         subject => subject.ID,
         (s, subject) => new { s.student, s.ss, subject })
     .Join(_context.Rooms,
         subj => subj.subject.Room_ID,
         room => room.ID,
         (subj, room) => new { subj.student, subj.ss, subj.subject, room })
     .Join(_context.Lecture,
         subj => subj.subject.ID,
         lecture => lecture.Sub_ID,
         (subj, lecture) => new { subj.student, subj.ss, subj.subject, subj.room, lecture })
     .Join(_context.Attendance,
         sl => new { sl.student.ID, LectureID = sl.lecture.ID },
         att => new { ID = att.St_ID, att.LectureID },
         (sl, att) => new { sl.student, sl.subject, sl.room, sl.lecture, att })
     .Join(_context.Doctors,
         s => s.subject.Dr_ID,
         doc => doc.ID,
         (s, doc) => new
         {
             s.student.St_NameAr,
             s.student.St_NameEn,
             s.subject.Sub_Name,
             doc.Dr_NameAr,
             doc.Dr_NameEn,
             s.room.Room_Num,
             Atten = s.att.Atten,
             Timestamp = s.att.Timestamp
         })
     .ToListAsync(); // نفذ الاستعلام أولاً

            // ثم رتب النتائج في الذاكرة
            var orderedResult = result
                .OrderBy(x => x.Timestamp.DayOfWeek)
                .Select(x => new
                {
                    x.St_NameAr,
                    x.St_NameEn,
                    x.Sub_Name,
                    x.Dr_NameAr,
                    x.Dr_NameEn,
                    x.Room_Num,
                    x.Atten,
                    FingerTime = x.Timestamp.DayOfWeek.ToString()
                })
                .ToList();

            return Ok(orderedResult); //200
        }

        [HttpGet("AttendanceStudentFinger")]
        public async Task<ActionResult<Attendance_T>> Attendance(int Finger_ID,int Lecture_Id, DateTime NowDate)
        { 
            try
            {
                var Studet = await _unitOfWork.Repository<Students>().GetByIdFinger(Finger_ID);
                if (Studet == null)
                {
                    return NotFound(new ApiResponse(404));// 404
                }
                var St_ID = Studet.ID;
                var mappedAttendance = new Attendance_T
                {
                    ID = 0,
                    LectureID = Lecture_Id,
                    St_ID = St_ID,
                    Timestamp = NowDate,
                    Atten =  true, 
                };
                var data = await _unitOfWork.Repository<Attendance_T>().AddAsync(mappedAttendance);
                await _unitOfWork.CompleteAsync(); 
                return Ok(data); // 200
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);// 400
            }

        }

    }
}
