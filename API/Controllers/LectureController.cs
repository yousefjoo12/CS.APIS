using API.DTOs;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Repositories.Contract;
using Core.Specifications.SubjectsSpecifications;
using Core.Specifications.SubjectsSpecParamsSpecifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.APIS.Erorrs;
using Repository.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Controllers
{
    public class LectureController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly StoreContext _context;

        public LectureController(IUnitOfWork unitOfWork, IMapper mapper, StoreContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }
        [HttpGet("GetAllLecture")]   //Lecture
        public async Task<ActionResult<IReadOnlyList<LectureDTO>>> GetAllLecture()
        {
            var lectures = from l in _context.Lecture
                           join sub in _context.Subjects on l.Sub_ID equals sub.ID
                           join d in _context.Doctors on sub.Dr_ID equals d.ID
                           join f in _context.Faculty on d.Fac_ID equals f.ID
                           where f.ID == 1
                           select new
                           {
                               l.Lecture_Name,
                               sub.Sub_Name,
                               d.Dr_NameAr,
                               f.Fac_Name,
                               l.day,
                               l.FromTime,
                               l.ToTime
                           };
            return Ok(lectures); //200
        }
        [HttpPost("Add_OR_UpdateLecture")]
        public async Task<ActionResult<Lecture_S>> AddSubject(LectureDTO Lecture_S)
        {
            var mappedLecture = new Lecture_S
            {
                ID = Lecture_S.ID,
                Lecture_Name = Lecture_S.Lecture_Name,
                Sub_ID = Lecture_S.Sub_ID,
                day = Lecture_S.day,
                FromTime = Lecture_S.FromTime,
                ToTime = Lecture_S.ToTime
            };
            var GetLecture = await _unitOfWork.Repository<Lecture_S>().GetById(Lecture_S.ID);
            if (GetLecture != null)
            {
                var data = await _unitOfWork.Repository<Lecture_S>().UpdateAsync(mappedLecture);
                if (data is null) return BadRequest(new ApiResponse(400));
                await _unitOfWork.CompleteAsync();
                return Ok(data);
            }
            else
            {
                mappedLecture.ID = 0;
                var data = await _unitOfWork.Repository<Lecture_S>().AddAsync(mappedLecture);
                if (data is null) return BadRequest(new ApiResponse(400));
                await _unitOfWork.CompleteAsync();
                return Ok(data);
            }

        }
        [HttpDelete("DeleteLecture")]
        public async Task DeleteLecture_S(int id)
        {
            var Subject = await _unitOfWork.Repository<Lecture_S>().GetById(id);
            if (Subject is not null)
            {
                _unitOfWork.Repository<Lecture_S>().Delete(Subject);
                await _unitOfWork.CompleteAsync();
            }
            else
            {
                NotFound(new ApiResponse(404));// 404
            }

        }
    }
}
