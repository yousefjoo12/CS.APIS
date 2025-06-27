using API.DTOs;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Entities.Identity;
using Core.Repositories.Contract;
using Core.Services.Contract;
using Core.Specifications.studetsSpecifications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.APIS.Erorrs;
using Repository.Data;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Controllers
{
    public class StudetsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly StoreContext context;
        private readonly UserManager<AppUser> _userManager;

        public StudetsController(IUnitOfWork unitOfWork, IMapper mapper, StoreContext context, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this.context = context;
            _userManager = userManager;
        }

        [HttpGet("GetStudetByEmail")]
        public async Task<ActionResult<Students>> GetStudetByEmail(string Email)
        {
            try
            {
                var Studet = await _unitOfWork.Repository<Students>().GetByEmail(Email);

                if (Studet == null)
                {
                    return NotFound(new ApiResponse(404));// 404
                }
                var data = _mapper.Map<Students, StudentsDTO>(Studet);
                return Ok(data); // 200
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);// 400
            }

        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        [HttpGet("GetAllStudets")]   //Studets
        public async Task<ActionResult<IReadOnlyList<StudentsDTO>>> GetAllStudets()
        {
            var query = context.Students
            .Include(s => s.FacultyYearSemister)
            .ThenInclude(fys => fys.FacultyYear)
            .ThenInclude(fy => fy.Faculty)
            .Select(s => new
            {
                s.FacultyYearSemister.Sem_Code,
                s.FacultyYearSemister.FacultyYear.Year,
                s.FacultyYearSemister.FacultyYear.Faculty.Fac_Name,
                s.FacultyYearSemister.Sem_Name,
                s.St_Code,
                s.St_NameAr,
                s.ID,
                s.St_NameEn,
                s.St_Email,
                s.St_Image,
                s.Phone,
                s.FingerID,
                s.FacYearSem_ID,
                s.FacultyYearSemister.FacultyYear.Faculty.Fac_Code
            });

            return Ok(query); //200

        }
        [HttpGet("DashBordStudets")]   //Studets
        public async Task<ActionResult<IReadOnlyList<StudentsDTO>>> DashBordStudets(string Email)
        {
            var query = from s in context.Students
                        join ss in context.Studets_Subject on s.ID equals ss.St_ID
                        join sub in context.Subjects on ss.Sub_ID equals sub.ID
                        join r in context.Rooms on sub.Room_ID equals r.ID
                        join d in context.Doctors on sub.Dr_ID equals d.ID
                        join l in context.Lecture on sub.ID equals l.Sub_ID
                        where s.St_Email == Email
                        select new
                        {
                            s.St_Code,
                            s.St_NameAr,
                            Sub_Name = sub.Sub_Name,
                            Room_Num = r.Room_Num,
                            Dr_NameAr = d.Dr_NameAr,
                            Day = l.day,
                            StartData = l.FromTime,
                            EndTime = l.ToTime
                        };

            var result = query.ToList();

            return Ok(result); //200
        }
        [HttpGet("GetStudentFinger")]
        public async Task<ActionResult<Students>> GetStudetByIdFinger(int id)
        {
            try
            {
                var Studet = await _unitOfWork.Repository<Students>().GetByIdFinger(id);
                if (Studet == null)
                {
                    return NotFound(new ApiResponse(404));// 404
                }
                var data = _mapper.Map<Students, StudentsDTO>(Studet);
                return Ok(data); // 200
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);// 400
            }

        }

        [HttpPost("Add_OR_UpdateStudent")]
        public async Task<ActionResult<Students>> AddStudet(StudentsDTO students)
        {
            //var mappedStudents = _mapper.Map<StudentsDTO, Students>(students);  

            var mappedStudents = new Students
            {
                ID = students.ID,
                St_Code = students.St_Code,
                St_NameAr = students.St_NameAr,
                St_NameEn = students.St_NameEn,
                St_Email = students.St_Email,
                FingerID = students.FingerID,
                St_Image = students.St_Image,
                Phone = students.Phone,
                FacYearSem_ID = students.FacYearSem_ID,
            };
            if (mappedStudents.ID != 0)
            {
                var data = await _unitOfWork.Repository<Students>().UpdateAsync(mappedStudents);
                if (data is null) return BadRequest(new ApiResponse(400));
                await _unitOfWork.CompleteAsync();
                return Ok(data);
            }
            else
            {
                mappedStudents.ID = 0;
                var data = await _unitOfWork.Repository<Students>().AddAsync(mappedStudents);
                if (data is null) return BadRequest(new ApiResponse(400));
                await _unitOfWork.CompleteAsync();
                return Ok(data);
            }


        }
        [HttpPost("AddAllSubjectForOneStudents")]
        public async Task<IActionResult> AddAllSubjectForOneStudents()
        {
            var studentIds = await context.Students.Select(x => x.ID).ToListAsync();
            var subjectIds = await context.Subjects.Select(x => x.ID).ToListAsync();

            var existingMappings = await context.Studets_Subject
                .Select(x => new { x.St_ID, x.Sub_ID })
                .ToListAsync();

            var existingSet = new HashSet<(int, int)>(
                existingMappings.Select(x => (x.St_ID, x.Sub_ID))
            );

            var allMappings = new List<Studets_Subject>();

            foreach (var studentId in studentIds)
            {
                foreach (var subjectId in subjectIds)
                {
                    if (!existingSet.Contains((studentId, subjectId)))
                    {
                        allMappings.Add(new Studets_Subject
                        {
                            St_ID = studentId,
                            Sub_ID = subjectId
                        });
                    }
                }
            }

            await _unitOfWork.Repository<Studets_Subject>().AddRangeAsync(allMappings);
            await _unitOfWork.CompleteAsync();

            return Ok(new
            {
                Message = $"Added {allMappings.Count} new student-subject mappings."
            });
        }



        [HttpDelete("DeleteStudent")]
        public async Task DeleteStudents(int id)
        {
            var Studet = await _unitOfWork.Repository<Students>().GetById(id);
            if (Studet is not null)
            {
                _unitOfWork.Repository<Students>().Delete(Studet);
                await _unitOfWork.CompleteAsync();

            }
            else
            {
                NotFound(new ApiResponse(404));// 404
            }

        }
        [HttpGet("GetStudentsCount")]
        public async Task<ActionResult<int>> GetStudentCount()
        {
            try
            {
                var studentCount = await context.Students.CountAsync();
                return Ok(studentCount);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(500, $"An error occurred while retrieving student count: {ex.Message}"));
            }

        }
    }
}
