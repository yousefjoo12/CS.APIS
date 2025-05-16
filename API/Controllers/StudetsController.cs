using API.DTOs;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Repositories.Contract;
using Core.Specifications.studetsSpecifications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.APIS.Erorrs;
using Repository.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Controllers
{
    public class StudetsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudetsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]  
        [HttpGet]   //Studets
        public async Task<ActionResult<IReadOnlyList<StudentsDTO>>> GetAllStudets()
        {
            var Studets = await _unitOfWork.Repository<Students>().GetAll();
            var data = _mapper.Map<IReadOnlyList<Students>, IReadOnlyList<StudentsDTO>>(Studets);
            return Ok(data); //200
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Students>> GetStudet(int id)
        {
            try
            {
                var Studet = await _unitOfWork.Repository<Students>().GetById(id);
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

        [HttpPost("add-student")]

        public async Task<ActionResult<Students>> AddStudet(StudentsDTO students)
        {
            //var mappedStudents = _mapper.Map<StudentsDTO, Students>(students);  

            var mappedStudents = new Students
            {
                ID = students.ID,
                St_NameAr = students.St_NameAr,
                St_NameEn = students.St_NameEn,
                St_Email = students.St_Email,
                St_Image = students.St_Image,
                Phone = students.Phone,
                Fac_ID = students.Fac_ID,
                FacYearSem_ID = students.FacYearSem_ID,
            }; 
            var data = await _unitOfWork.Repository<Students>().AddAsync(mappedStudents);
            if (data is null) return BadRequest(new ApiResponse(400));
            await _unitOfWork.CompleteAsync();
            return Ok(data);

        }



        [HttpPut]
        public async Task<ActionResult<Students>> UpdateStudet(StudentsDTO students)
        {
            // mapping  => from Dto[StudentsDTO] to model[Students]
            var mappedStudents = _mapper.Map<StudentsDTO, Students>(students);
            var data = _unitOfWork.Repository<Students>().UpdateAsync(mappedStudents);
            if (data is null) return BadRequest(new ApiResponse(400));
            await _unitOfWork.CompleteAsync();
            return Ok(mappedStudents);

        }
        [HttpDelete]
        public async Task DeleteStudents(int id)
        {
            var Spec = new studetsWithSubjectSpecifications(id);
            var Studet = await _unitOfWork.Repository<Students>().GetWithspecAsync(Spec);
            _unitOfWork.Repository<Students>().Delete(Studet);
            await _unitOfWork.CompleteAsync();

        }

    }
}
