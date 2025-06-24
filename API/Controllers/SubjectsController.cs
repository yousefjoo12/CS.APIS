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

        [HttpGet("GetAllSubjects")]
        public async Task<ActionResult<IReadOnlyList<SubjectsDTO>>> GetAllSubjects()
        {
            var query = _context.Subjects
                .Include(s => s.FacultyYearSemister)
                    .ThenInclude(fys => fys.FacultyYear)
                        .ThenInclude(fy => fy.Faculty)
                .Include(s => s.Doctors)
                .Select(s => new
                {
                    SubCode = s.Sub_Code,
                    SubName = s.Sub_Name,
                    Doctor = s.Doctors.Dr_NameAr,
                    Faculty = s.FacultyYearSemister.FacultyYear.Faculty.Fac_Name,
                    Year = s.FacultyYearSemister.FacultyYear.Year,
                    Semister = s.FacultyYearSemister.Sem_Name
                })
                .ToList();

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
                    return NotFound(new ApiResponse(404));
                }
                var data = _mapper.Map<Subjects, SubjectsDTO>(Subject);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Add_OR_UpdateSubject")]
        public async Task<ActionResult<Subjects>> AddSubject([FromBody] SubjectsDTO Subjects)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new
                    {
                        Field = x.Key,
                        Errors = x.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    });

                return BadRequest(new
                {
                    statusCode = 400,
                    message = "Validation Failed",
                    errors = allErrors
                });
            }

            Console.WriteLine($"📥 Incoming Subject: {Subjects.Sub_Name} / Dr_ID: {Subjects.Dr_ID} / Sem_ID: {Subjects.FacYearSem_ID} / Room_ID: {Subjects.Room_ID}");

            var mappedSubjects = new Subjects
            {
                ID = Subjects.ID,
                Sub_Code = Subjects.Sub_Code,
                Sub_Name = Subjects.Sub_Name,
                Dr_ID = Subjects.Dr_ID,
                FacYearSem_ID = Subjects.FacYearSem_ID,
                Room_ID = Subjects.Room_ID 
            };

            try
            {
                if (mappedSubjects.ID != 0)
                {
                    var data = await _unitOfWork.Repository<Subjects>().UpdateAsync(mappedSubjects);
                    if (data is null) return BadRequest(new ApiResponse(400));
                    await _unitOfWork.CompleteAsync();
                    return Ok(data);
                }
                else
                {
                    mappedSubjects.ID = 0;
                    var data = await _unitOfWork.Repository<Subjects>().AddAsync(mappedSubjects);
                    if (data is null) return BadRequest(new ApiResponse(400));
                    await _unitOfWork.CompleteAsync();
                    return Ok(data);
                }
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine("❌ Save Failed: " + innerMessage);

                return StatusCode(500, new
                {
                    statusCode = 500,
                    message = "An error occurred while saving the entity changes. See the inner exception for details.",
                    details = innerMessage
                });
            }
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
                NotFound(new ApiResponse(404));
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
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse(500, $"An error occurred while retrieving subject count: {ex.Message}"));
            }
        }
    }
}
