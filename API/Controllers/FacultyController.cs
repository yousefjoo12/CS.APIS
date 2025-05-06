using API.DTOs;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Repositories.Contract;
using Core.Specifications.FacultySpecifications;
using Core.Specifications.FacultySpecifications;
using Core.Specifications.studetsSpecifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.APIS.Erorrs;
using Project.Core.Specifications;


namespace API.Controllers
{
    public class FacultyController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FacultyController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(Faculty), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]    //Faculty
        public async Task<ActionResult<IReadOnlyList<Faculty>>> GetAllFaculty([FromQuery] FacultySpecParams Facultysarams)
        {
            var Spec = new FacultyWithSpecifications(Facultysarams);
            var Faculty = await _unitOfWork.Repository<Faculty>().GetAllWithSpecAsync(Spec); 
            return Ok(Faculty); //200
        }


        [ProducesResponseType(typeof(Faculty), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Faculty>> GetFaculty(int id)
        {
            var Spec = new FacultyWithSpecifications(id);
            var Faculty = await _unitOfWork.Repository<Faculty>().GetWithspecAsync(Spec);
            if (Faculty == null)
            {
                return NotFound(new ApiResponse(404));// 404
            }
            return Ok(Faculty); // 200
        }

    }
}
