using API.DTOs;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Repositories.Contract;
using Core.Specifications.studetsSpecifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.APIS.Erorrs;

namespace API.Controllers
{
    public class StudetsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudetsController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(Students), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]//  /Studets
        public async Task<ActionResult<IReadOnlyList<StudentsDTO>>> GetAllStudets([FromQuery] studetsSpecParams studetsParams )
        {
            var Spec = new studetsWithSubjectSpecifications(studetsParams);
            var Studets = await _unitOfWork.Repository<Students>().GetAllWithSpecAsync(Spec);
            var data = _mapper.Map<IEnumerable<Students>, IEnumerable<StudentsDTO>>(Studets);

            return Ok(data); //200
        }


        [ProducesResponseType(typeof(Students), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Students>> GetStudet(int id)
        {
            var Spec = new studetsWithSubjectSpecifications(id);
            var Studet = await _unitOfWork.Repository<Students>().GetWithspecAsync(Spec);
            var data = _mapper.Map<Students, StudentsDTO>(Studet);
            if (data == null)
            {
                return NotFound(new ApiResponse(404));// 404
            }
            return Ok(data); // 200
        }

    }
}
