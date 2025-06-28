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
using System.Linq;

namespace API.Controllers
{
    public class SubjectsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly StoreContext _context;

        public SubjectsController(IUnitOfWork unitOfWork, IMapper mapper, StoreContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]  
        [HttpGet("GetAllSubjects")]   //Subjects
        public async Task<ActionResult<IReadOnlyList<object>>> GetAllSubjects()
        {
            var query = await _context.Subjects
                .Include(s => s.FacultyYearSemister)
                    .ThenInclude(fys => fys.FacultyYear)
                        .ThenInclude(fy => fy.Faculty)
                .Include(s => s.Doctors)
                .Select(s => new
                {
                    id = s.ID,
                    subCode = s.Sub_Code,
                    subName = s.Sub_Name,
                    doctor = s.Doctors.Dr_NameAr,
                    faculty = s.FacultyYearSemister.FacultyYear.Faculty.Fac_Name,
                    year = s.FacultyYearSemister.FacultyYear.Year,
                    semister = s.FacultyYearSemister.Sem_Name,
                    facYearSem_ID = s.FacYearSem_ID,
                    room_ID = s.Room_ID,
                    dr_ID = s.Dr_ID
                })
                .ToListAsync();

            return Ok(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subjects>> GetSubject(int id)
        {
            try
            {
                var Subject = await _unitOfWork.Repository<Subjects>().GetById(id);
                if (Subject == null)
                {
                    return NotFound(new ApiResponse(404));// 404
                }
                var data = _mapper.Map<Subjects, SubjectsDTO>(Subject);
                return Ok(data); // 200
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);// 400
            }

        }

        [HttpPost("Add_OR_UpdateSubject")]
        public async Task<ActionResult<Subjects>> AddSubject([FromBody] SubjectsDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));

            var subject = new Subjects
            {
                ID = dto.ID,
                Sub_Code = dto.Sub_Code,
                Sub_Name = dto.Sub_Name,
                Dr_ID = dto.Dr_ID,
                FacYearSem_ID = dto.FacYearSem_ID,
                Room_ID = dto.Room_ID
            };

            if (dto.ID != 0)
            {
                var updated = await _unitOfWork.Repository<Subjects>().UpdateAsync(subject);
                if (updated is null) return BadRequest(new ApiResponse(400));
                await _unitOfWork.CompleteAsync();
                return Ok(updated);
            }
            else
            {
                var added = await _unitOfWork.Repository<Subjects>().AddAsync(subject);
                await AddAllSubjectForOneStudents(subject.FacYearSem_ID);
                if (added is null) return BadRequest(new ApiResponse(400));
                await _unitOfWork.CompleteAsync();
                return Ok(added);
            }



        }
        [HttpPost("AddAllSubjectForOneStudents")]
        public async Task<IActionResult> AddAllSubjectForOneStudents(int semesterId)
        {

            var studentIds = await _context.Students
                .Where(x => x.FacYearSem_ID == semesterId)
                .Select(x => x.ID)
                .ToListAsync();

            if (!studentIds.Any())
            {
                return BadRequest(new { Message = "No students found for the specified semester." });
            }

            var subjectIds = await _context.Subjects
                .Where(x => x.FacYearSem_ID == semesterId)
                .Select(x => x.ID)
                .ToListAsync();

            if (!subjectIds.Any())
            {
                return BadRequest(new { Message = "No subjects found for the specified semester." });
            }

            // 3. جلب العلاقات الموجودة مسبقًا (فقط للطلاب والمواد المحددة)
            var existingMappings = await _context.Studets_Subject
                .Where(x => studentIds.Contains(x.St_ID) && subjectIds.Contains(x.Sub_ID))
                .Select(x => new { x.St_ID, x.Sub_ID })
                .ToListAsync();

            var existingSet = new HashSet<(int, int)>(existingMappings.Select(x => (x.St_ID, x.Sub_ID)));

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
                Message = $"Added {allMappings.Count} new student-subject mappings for semester {semesterId}."
            });
        }
        [HttpDelete("DeleteSubject")]
        public async Task DeleteSubjects(int id)
        {
            var Subject = await _unitOfWork.Repository<Subjects>().GetById(id);
            if (Subject is not null)
            {
                _unitOfWork.Repository<Subjects>().Delete(Subject);
                await _unitOfWork.CompleteAsync();
            }
            else
            {
                NotFound(new ApiResponse(404));// 404
            }

        }
        [HttpGet("GetSubjectsCount")]
        public async Task<ActionResult<int>> GetSubjectCount()
        {
            try
            {
                var Subject = _context.Subjects.Count();
                return Ok(Subject);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(500, $"An error occurred while retrieving student count: {ex.Message}"));
            }
        }
    }
}