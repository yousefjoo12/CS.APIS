using API.DTOs;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Repositories.Contract;
using Core.Specifications.FacultyYearSpecifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.APIS.Erorrs;
using Project.Core.Specifications;


namespace API.Controllers
{
    public class FacultyYearController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FacultyYearController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(FacultyYear), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]    //FacultyYear
        public async Task<ActionResult<IReadOnlyList<FacultyYear>>> GetAllFacultyYear([FromQuery] FacultyYearSpecParams FacultyYearsarams)
        {
            var Spec = new FacultyYearWithSpecifications(FacultyYearsarams);
            var FacultyYear = await _unitOfWork.Repository<FacultyYear>().GetAllWithSpecAsync(Spec); 
            return Ok(FacultyYear); //200
        }


        [ProducesResponseType(typeof(FacultyYear), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<FacultyYear>> GetFacultyYear(int id)
        {
            var Spec = new FacultyYearWithSpecifications(id);
            var FacultyYear = await _unitOfWork.Repository<FacultyYear>().GetWithspecAsync(Spec);
            if (FacultyYear == null)
            {
                return NotFound(new ApiResponse(404));// 404
            }
            return Ok(FacultyYear); // 200
        }

    }
}
