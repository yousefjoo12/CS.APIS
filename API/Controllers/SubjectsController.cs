using API.DTOs;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Repositories.Contract; 
using Core.Specifications.SubjectsSpecifications;
using Core.Specifications.SubjectsSpecParamsSpecifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.APIS.Erorrs;

namespace API.Controllers
{
    public class SubjectsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubjectsController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(Subjects), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]//  /Subjects 
        public async Task<ActionResult<IReadOnlyList<SubjectsDTO>>> GetAllSubjects([FromQuery] SubjectsSpecParams SubjectsParams)
        {
            var Spec = new SubjectsWithSpecifications(SubjectsParams);
            var Subjects = await _unitOfWork.Repository<Subjects>().GetAllWithSpecAsync(Spec);
            var data = _mapper.Map<IEnumerable<Subjects>, IEnumerable<SubjectsDTO>>(Subjects);

            return Ok(data); //200
        }


        [ProducesResponseType(typeof(Subjects), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Subjects>> GetSubject(int id)
        {
            var Spec = new SubjectsWithSpecifications(id);
            var Subject = await _unitOfWork.Repository<Subjects>().GetWithspecAsync(Spec);
            var data = _mapper.Map<Subjects, SubjectsDTO>(Subject);
            if (data == null)
            {
                return NotFound(new ApiResponse(404));// 404
            }
            return Ok(data); // 200
        }
		[HttpPost]
		public async Task<ActionResult<Subjects>> AddSubject(SubjectsDTO Subjects)
		{
			// mapping  => from Dto[SubjectsDTO] to model[Subjects]
			var mappedSubjects = _mapper.Map<SubjectsDTO, Subjects>(Subjects);
			var data = await _unitOfWork.Repository<Subjects>().AddAsync(mappedSubjects);
			if (data is null) return BadRequest(new ApiResponse(400));
			await _unitOfWork.CompleteAsync();
			return Ok(data);

		}

		[HttpPut]
		public async Task<ActionResult<Subjects>> UpdateSubject(SubjectsDTO Subjects)
		{
			// mapping  => from Dto[SubjectsDTO] to model[Subjects]
			var mappedSubjects = _mapper.Map<SubjectsDTO, Subjects>(Subjects);
			var data = _unitOfWork.Repository<Subjects>().UpdateAsync(mappedSubjects);
			if (data is null) return BadRequest(new ApiResponse(400));
			await _unitOfWork.CompleteAsync();
			return Ok(mappedSubjects); 
		}
		[HttpDelete]
		public async Task DeleteSubjects(int id)
		{
			var Spec = new SubjectsWithSpecifications(id);
			var Subject = await _unitOfWork.Repository<Subjects>().GetWithspecAsync(Spec);
			_unitOfWork.Repository<Subjects>().DeleteAsync(Subject);
			await _unitOfWork.CompleteAsync();

		}

	}
}
