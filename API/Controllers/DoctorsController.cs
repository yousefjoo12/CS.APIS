using API.DTOs;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Repositories.Contract;
using Core.Specifications.DoctorsSpecifications; 
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

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]  
        [HttpGet("GetAllDoctors")]   //Doctors
        public async Task<ActionResult<IReadOnlyList<DoctorsDTO>>> GetAllDoctors()
        {
            var Doctors = await _unitOfWork.Repository<Doctors>().GetAll();
            var data = _mapper.Map<IReadOnlyList<Doctors>, IReadOnlyList<DoctorsDTO>>(Doctors);
            return Ok(data); //200
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctors>> GetDoctor(int id)
        {
            try
            {
                var Doctor = await _unitOfWork.Repository<Doctors>().GetById(id);
                if (Doctor == null)
                {
                    return NotFound(new ApiResponse(404));// 404
                }
                var data = _mapper.Map<Doctors, DoctorsDTO>(Doctor);
                return Ok(data); // 200
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);// 400
            }

        }

        [HttpPost("Add_OR_UpdateDoctor")]
        public async Task<ActionResult<Doctors>> AddDoctor(DoctorsDTO Doctors)
        {
            //var mappedDoctors = _mapper.Map<DoctorsDTO, Doctors>(Doctors);  

            var mappedDoctors = new Doctors
            {
                ID = Doctors.ID,
                Dr_Code = Doctors.Dr_Code,
                Dr_NameAr = Doctors.Dr_NameAr,
                Dr_NameEn = Doctors.Dr_NameEn,
                Dr_Email = Doctors.Dr_Email,
                Dr_Image = Doctors.Dr_Image,
                Phone = Doctors.Phone,
            };
            if (mappedDoctors.ID != 0)
            {
                var data = await _unitOfWork.Repository<Doctors>().UpdateAsync(mappedDoctors);
                if (data is null) return BadRequest(new ApiResponse(400));
                await _unitOfWork.CompleteAsync();
                return Ok(data);
            }
            else
            {
                mappedDoctors.ID = 0;
                var data = await _unitOfWork.Repository<Doctors>().AddAsync(mappedDoctors);
                if (data is null) return BadRequest(new ApiResponse(400));
                await _unitOfWork.CompleteAsync();
                return Ok(data);
            }


        }
        [HttpDelete("DeleteDoctor")]
        public async Task DeleteDoctors(int id)
        {
            var Doctor = await _unitOfWork.Repository<Doctors>().GetById(id);
            if (Doctor is not null)
            {
                _unitOfWork.Repository<Doctors>().Delete(Doctor);
                await _unitOfWork.CompleteAsync();

            }
            else
            {
                NotFound(new ApiResponse(404));// 404
            }

        }
    }
}
