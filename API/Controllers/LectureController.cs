using API.DTOs;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Enums;
using Core.Repositories.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.APIS.Erorrs;
using Repository.Data;

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

        [HttpGet("GetAllLecture")]
        public async Task<ActionResult<IReadOnlyList<LectureDTO>>> GetAllLecture()
        {
            var lectures = from l in _context.Lecture
                           join sub in _context.Subjects on l.Sub_ID equals sub.ID
                           join d in _context.Doctors on sub.Dr_ID equals d.ID
                           join f in _context.Faculty on d.Fac_ID equals f.ID
                           where f.ID == 1
                           select new LectureDTO
                           {
                               Id = l.ID,
                               Sub_ID = sub.ID,
                               Subjects = sub.Sub_Name,
                               Day = (int)l.day,
                               FromTime = l.FromTime.ToString(@"hh\:mm\:ss"),
                               ToTime = l.ToTime.ToString(@"hh\:mm\:ss")
                           };

            return Ok(await lectures.ToListAsync());
        }

        [HttpPost("Add_OR_UpdateLecture")]
        public async Task<ActionResult<Lecture_S>> AddSubject(LectureDTO dto)
        {
            try
            {
                if (dto.Sub_ID == 0)
                    return BadRequest(new ApiResponse(400, "Subject ID cannot be 0"));

                var lectureEntity = new Lecture_S
                {
                    ID = dto.Id,
                    Sub_ID = dto.Sub_ID,
                    day = (Days)dto.Day,
                    FromTime = TimeSpan.Parse(dto.FromTime),
                    ToTime = TimeSpan.Parse(dto.ToTime)
                };

                if (dto.Id != 0)
                {
                    var existing = await _unitOfWork.Repository<Lecture_S>().GetById(dto.Id);
                    if (existing == null) return NotFound(new ApiResponse(404, "Lecture not found"));

                    // Manual update
                    existing.Sub_ID = lectureEntity.Sub_ID;
                    existing.day = lectureEntity.day;
                    existing.FromTime = lectureEntity.FromTime;
                    existing.ToTime = lectureEntity.ToTime;

                    await _unitOfWork.CompleteAsync();
                    return Ok(existing);
                }
                else
                {
                    var added = await _unitOfWork.Repository<Lecture_S>().AddAsync(lectureEntity);
                    await _unitOfWork.CompleteAsync();
                    return Ok(added);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Failed to save lecture",
                    error = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpDelete("DeleteLecture")]
        public async Task<IActionResult> DeleteLecture_S(int id)
        {
            var lecture = await _unitOfWork.Repository<Lecture_S>().GetById(id);
            if (lecture == null) return NotFound(new ApiResponse(404));

            _unitOfWork.Repository<Lecture_S>().Delete(lecture);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }
    }
}
