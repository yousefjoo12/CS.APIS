using API.DTOs;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Enums;
using Core.Repositories.Contract;
using Core.Specifications.SubjectsSpecifications;
using Core.Specifications.SubjectsSpecParamsSpecifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

        //[HttpGet("AttendanceStudentFinger")]
        //public async Task<ActionResult<Studets_Subject>> Attendance(int Finger_ID/*,int Lecture_Id, DateTime NowDate*/)
        //{ 
        //    try
        //    {

        //       // return Ok(StudetsSubjectsList); // 200
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);// 400
        //    }

        //}
        [HttpGet("AttendanceStudentFinger")]
        public async Task<ActionResult<IReadOnlyList<AttendanceDTO>>> GetStudentLectureIds(int fingerId, int roomId, DateTime Date)
        {

            string DayName = Date.DayOfWeek.ToString();  // "Monday", مثلاً
            Days today = (Days)Enum.Parse(typeof(Days), DayName); 
            int dayOfWeek = (int)today;

            var currentTime = Date.ToString("HH:mm");
            var Studet = await _unitOfWork.Repository<Students>().GetByIdFinger(fingerId);

            var parameters = new[]{
                   new SqlParameter("@St_Id", fingerId),new SqlParameter("@Room_Id", roomId), new SqlParameter("@DayOfWeek", dayOfWeek),new SqlParameter("@CurrentTime", currentTime)
            };

            var Query = await _context.Set<Lecture_S>()
                .FromSqlRaw(@"SELECT l.ID
                FROM Lecture l
                INNER JOIN Subjects s ON l.Sub_ID = s.ID
                INNER JOIN Studets_Subject ss ON s.ID = ss.Sub_ID
                INNER JOIN Students st ON ss.St_ID = st.id
                WHERE st.FingerID = @St_Id
                AND s.Room_ID = @Room_Id
                AND l.day = @DayOfWeek
                AND @CurrentTime BETWEEN l.FromTime AND l.ToTime", parameters)
               .Select(x => x.ID )
               .ToListAsync();
            var firstLectureId = Query.First();
            var mappedAttendance = new Attendance_T
            {
                ID = 0,
                LectureID = firstLectureId,
                St_ID = Studet.ID,
                Timestamp = Date,
                Atten = true,
            };
            var data = await _unitOfWork.Repository<Attendance_T>().AddAsync(mappedAttendance);
            await _unitOfWork.CompleteAsync(); 
            return Ok(data); 
        }

    }
}
