using API.DTOs;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Repositories.Contract;
using Core.Specifications.FacultyYearSemisterSpecifications;
using Core.Specifications.FacultyYearSemisterSpecParamsSpecifications;
using Core.Specifications.studetsSpecifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.APIS.Erorrs;
using Project.Core.Specifications;


namespace API.Controllers
{
    public class FacultyYearSemisterController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FacultyYearSemisterController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(FacultyYearSemister), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]    //FacultyYearSemister
        public async Task<ActionResult<IReadOnlyList<FacultyYearSemister>>> GetAllFacultyYearSemister([FromQuery] FacultyYearSemisterSpecParams FacultyYearSemistersarams)
        {
            var Spec = new FacultyYearSemisterWithSpecifications(FacultyYearSemistersarams);
            var FacultyYearSemister = await _unitOfWork.Repository<FacultyYearSemister>().GetAllWithSpecAsync(Spec); 
            return Ok(FacultyYearSemister); //200
        }


        [ProducesResponseType(typeof(FacultyYearSemister), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<FacultyYearSemister>> GetFacultyYearSemister(int id)
        {
            var Spec = new FacultyYearSemisterWithSpecifications(id);
            var FacultyYearSemister = await _unitOfWork.Repository<FacultyYearSemister>().GetWithspecAsync(Spec);
            if (FacultyYearSemister == null)
            {
                return NotFound(new ApiResponse(404));// 404
            }
            return Ok(FacultyYearSemister); // 200
        }

    }
}
