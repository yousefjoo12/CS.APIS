using API.DTOs;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Repositories.Contract;
using Core.Specifications.FacultyYearSemisterSpecParamsSpecifications;
using Core.Specifications.RoomsSpecifications;
using Core.Specifications.RoomsSpecParamsSpecifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.APIS.Erorrs;

namespace API.Controllers
{
    public class RoomsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomsController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(Rooms), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]//  /Rooms
        public async Task<ActionResult<IReadOnlyList<Rooms>>> GetAllRooms([FromQuery] RoomsSpecParams RoomsParams )
        {
            var Spec = new RoomsWithSpecifications(RoomsParams);
            var Rooms = await _unitOfWork.Repository<Rooms>().GetAllWithSpecAsync(Spec);
             
            return Ok(Rooms); //200
        }


        [ProducesResponseType(typeof(Rooms), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Rooms>> GetStudet(int id)
        {
            var Spec = new RoomsWithSpecifications(id);
            var Room = await _unitOfWork.Repository<Rooms>().GetWithspecAsync(Spec); 
            if (Room == null)
            {
                return NotFound(new ApiResponse(404));// 404
            }
            return Ok(Room); // 200
        }

    }
}
