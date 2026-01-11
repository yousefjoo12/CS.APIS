using API.DTOs;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Entities.Identity;
using Core.Repositories.Contract;
using Core.Services.Contract;
using Core.Specifications.studetsSpecifications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.APIS.Erorrs;
using Repository.Data;
using System.Globalization;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Controllers
{
    public class SchoolController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly StoreContext _context;
        private readonly UserManager<AppUser> _userManager;

        public SchoolController(IUnitOfWork unitOfWork, IMapper mapper, StoreContext context, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }

        ////[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
        [HttpGet("GetAllSchool")]   //School
        public async Task<ActionResult<IReadOnlyList<SchoolDTO>>> GetAllSchool()
        {
            var Schools = await _unitOfWork.Repository<School>().GetAll();
            var data = _mapper.Map<IReadOnlyList<School>, IReadOnlyList<SchoolDTO>>(Schools);
            return Ok(data); //200 
        }
        [HttpGet("{id}")]      
        public async Task<ActionResult<School>> GetSchool(int id)
        {
            try
            {
                var School = await _unitOfWork.Repository<School>().GetById(id);
                if (School == null)
                {
                    return NotFound(new ApiResponse(404));// 404
                }
                var data = _mapper.Map<School, SchoolDTO>(School);
                return Ok(data); // 200
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);// 400
            } 
        }
        [HttpPost("Add_OR_UpdateSchool")]
        public async Task<ActionResult<School>> AddDoctor(SchoolDTO Schools)
        {
            var mappedSchools = _mapper.Map<SchoolDTO, School>(Schools);  
            if (mappedSchools.ID != 0)
            {
                var data = await _unitOfWork.Repository<School>().UpdateAsync(mappedSchools);
                if (data is null) return BadRequest(new ApiResponse(400));
                await _unitOfWork.CompleteAsync();
                return Ok(data);
            }
            else
            {
                mappedSchools.ID = 0;
                var data = await _unitOfWork.Repository<School>().AddAsync(mappedSchools);
                if (data is null) return BadRequest(new ApiResponse(400));
                await _unitOfWork.CompleteAsync();
                return Ok(data);
            }


        }
    }
}
