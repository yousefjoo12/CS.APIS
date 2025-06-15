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
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Controllers
{
    public class LectureController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LectureController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("GetAllLecture")]   //Lecture
        public async Task<ActionResult<IReadOnlyList<LectureDTO>>> GetAllSubjects()
        {
            var Lectures = await _unitOfWork.Repository<Lecture_S>().GetAll();
            var data = _mapper.Map<IReadOnlyList<Lecture_S>, IReadOnlyList<LectureDTO>>(Lectures);
            return Ok(data); //200
        } 
        [HttpPost("Add_OR_UpdateLecture")]
        public async Task<ActionResult<Lecture_S>> AddSubject(LectureDTO Lecture_S)
        { 

            var mappedLecture_S = new Lecture_S
            {
                ID = Lecture_S.ID,
                Lecture_Name = Lecture_S.Lecture_Name,
                LectureDate = Lecture_S.LectureDate,
                Sub_ID = Lecture_S.Sub_ID, 
                Degree = Lecture_S.Degree
            };
            if (mappedLecture_S.ID != 0)
            {
                var data = await _unitOfWork.Repository<Lecture_S>().UpdateAsync(mappedLecture_S);
                if (data is null) return BadRequest(new ApiResponse(400));
                await _unitOfWork.CompleteAsync();
                return Ok(data);
            }
            else
            {
                mappedLecture_S.ID = 0;
                var data = await _unitOfWork.Repository<Lecture_S>().AddAsync(mappedLecture_S);
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
