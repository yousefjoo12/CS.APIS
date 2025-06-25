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
    public class NotificationController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllNotifications")]   //Rooms
        public async Task<ActionResult<IReadOnlyList<Notification>>> GetAllNotifications()
        {
            var Notifications = await _unitOfWork.Repository<Notification>().GetAll();
            var data = _mapper.Map<IReadOnlyList<Notification>, IReadOnlyList<NotificationDTO>>(Notifications);

            return Ok(data); //200
        }
        [HttpPost("AddNotifications")]
        public async Task<ActionResult<Notification>> AddNotifications(NotificationDTO Notification)
        {
            var mappedNotification = new Notification
            {
                ID = Notification.ID,
                Title = Notification.Title,
                Massage = Notification.Massage,
                FacYearSem_ID = Notification.FacYearSem_ID,
            };

            mappedNotification.ID = 0;
            var data = await _unitOfWork.Repository<Notification>().AddAsync(mappedNotification);
            if (data is null) return BadRequest(new ApiResponse(400));
            await _unitOfWork.CompleteAsync();
            return Ok(data);



        }
        [HttpDelete("DeleteRoom")]
        public async Task DeleteRooms(int id)
        {
            var Notification = await _unitOfWork.Repository<Notification>().GetById(id);
            if (Notification is not null)
            {
                _unitOfWork.Repository<Notification>().Delete(Notification);
                await _unitOfWork.CompleteAsync();
            }
            else
            {
                NotFound(new ApiResponse(404));// 404
            }

        }

    }
}
