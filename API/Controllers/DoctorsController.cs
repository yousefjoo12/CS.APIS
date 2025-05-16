using API.DTOs;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Repositories.Contract;
using Core.Specifications.DoctorsSpecifications;
using Core.Specifications.studetsSpecifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.APIS.Erorrs;
using Project.Core.Specifications;


namespace API.Controllers
{
    public class DoctorsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DoctorsController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(Doctors), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]    //Doctors
        public async Task<ActionResult<IReadOnlyList<Doctors>>> GetAllDoctors([FromQuery] DoctorsSpecParams Doctorsarams)
        {
            var Spec = new DoctorsWithSpecifications(Doctorsarams);
            var Doctors = await _unitOfWork.Repository<Doctors>().GetAllWithSpecAsync(Spec); 
            return Ok(Doctors); //200
        }


        [ProducesResponseType(typeof(Doctors), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctors>> GetDoctors(int id)
        {
            var Spec = new DoctorsWithSpecifications(id);
            var Doctor = await _unitOfWork.Repository<Doctors>().GetWithspecAsync(Spec);
            if (Doctor == null)
            {
                return NotFound(new ApiResponse(404));// 404
            }
            return Ok(Doctor); // 200
        }
		[HttpPost]
		public async Task<ActionResult<Doctors>> AddDoctor(DoctorsDTO doctors)
		{
			// mapping  => from Dto[DoctorsDTO] to model[Doctors]
			var mappedDoctors = _mapper.Map<DoctorsDTO, Doctors>(doctors);
			var data = await _unitOfWork.Repository<Doctors>().AddAsync(mappedDoctors);
			if (data is null) return BadRequest(new ApiResponse(400));
			await _unitOfWork.CompleteAsync();
			return Ok(data);

		}

		[HttpPut]
		public async Task<ActionResult<Doctors>> UpdateDoctor(DoctorsDTO doctors)
		{
			// mapping  => from Dto[DoctorsDTO] to model[Doctors]
			var mappedDoctors = _mapper.Map<DoctorsDTO, Doctors>(doctors);
			var data = _unitOfWork.Repository<Doctors>().UpdateAsync(mappedDoctors);
			if (data is null) return BadRequest(new ApiResponse(400));
			await _unitOfWork.CompleteAsync();
			return Ok(mappedDoctors);

		}
		[HttpDelete]
		public async Task DeleteDoctor(int id)
		{
			var Spec = new DoctorsWithSpecifications(id);
			var Doctor = await _unitOfWork.Repository<Doctors>().GetWithspecAsync(Spec);
			_unitOfWork.Repository<Doctors>().Delete(Doctor);
			await _unitOfWork.CompleteAsync();

		}
	}
}
