using API.DTOs;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Repositories.Contract;
using Core.Specifications.InstructorsSpecifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.APIS.Erorrs;
using Project.Core.Specifications;


namespace API.Controllers
{
    public class InstructorsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InstructorsController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(Instructors), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]    //Instructors
        public async Task<ActionResult<IReadOnlyList<Instructors>>> GetAllInstructors([FromQuery] InstructorsSpecParams Instructorssarams)
        {
            var Spec = new InstructorsWithSpecifications(Instructorssarams);
            var Instructors = await _unitOfWork.Repository<Instructors>().GetAllWithSpecAsync(Spec); 
            return Ok(Instructors); //200
        }


        [ProducesResponseType(typeof(Instructors), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Instructors>> GetInstructors(int id)
        {
            var Spec = new InstructorsWithSpecifications(id);
            var Instructors = await _unitOfWork.Repository<Instructors>().GetWithspecAsync(Spec);
            if (Instructors == null)
            {
                return NotFound(new ApiResponse(404));// 404
            }
            return Ok(Instructors); // 200
        }

    }
}
