using API.DTOs;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Repositories.Contract;
using Core.Specifications.DoctorsSpecifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.APIS.Erorrs;
using Project.Core.Specifications;
using Repository.Data;
using System.Linq;

namespace API.Controllers
{
    public class DoctorsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly StoreContext context;

        public DoctorsController(IUnitOfWork unitOfWork, IMapper mapper, StoreContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this.context = context;
        }

        [HttpGet("GetDoctorByEmail")]
        public async Task<ActionResult<Doctors>> GetDoctorByEmail(string Email)
        {
            try
            {
                var Doctor = await _unitOfWork.Repository<Doctors>().GetByEmail(Email);
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

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]  
        [HttpGet("GetAllDoctors")]   //Doctors
        public async Task<ActionResult<IReadOnlyList<DoctorsDTO>>> GetAllDoctors()
        {
            var Doctors = await _unitOfWork.Repository<Doctors>().GetAll();
            var data = _mapper.Map<IReadOnlyList<Doctors>, IReadOnlyList<DoctorsDTO>>(Doctors);
            return Ok(data); //200
        }

        [HttpGet("DashBordDoctors")]   //Doctors
        public async Task<ActionResult<IReadOnlyList<DoctorsDTO>>> DashBordDoctors(string Dr_Email)
        {

            var result = context.Doctors
                      .Where(d => d.Dr_Email == Dr_Email)
                      .Join(context.Subjects, d => d.ID, s => s.Dr_ID, (d, s) => new { d, s })
                      .Join(context.Rooms, ds => ds.s.Room_ID, r => r.ID, (ds, r) => new { ds.d, ds.s, r })
                      .Join(context.Lecture, dsr => dsr.s.ID, l => l.Sub_ID, (dsr, l) => new { dsr.d, dsr.s, dsr.r, l })
                      .Join(context.Studets_Subject, dsrl => dsrl.s.ID, ss => ss.Sub_ID, (dsrl, ss) => new { dsrl.d, dsrl.s, dsrl.r, dsrl.l, ss })
                      .AsEnumerable()
                      .GroupBy(x => new
                      {
                          x.d.Dr_NameAr,
                          x.s.Sub_Name,
                          Day = x.l.day, 
                          x.r.Room_Num
                      })
                      .Select(g => new
                      {
                          Dr_NameAr = g.Key.Dr_NameAr,
                          Sub_Name = g.Key.Sub_Name,
                          Day = g.Key.Day.ToString(),  
                          Room_Num = g.Key.Room_Num,
                          TotalStudents = g.Count()
                      }).ToList();

            return Ok(result);

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
                Fac_ID = Doctors.Fac_ID,
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
