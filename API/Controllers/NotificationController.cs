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
            Console.WriteLine($"🟢 Received DTO - Title: {Notification?.Title}, Massage: {Notification?.Massage}, FacYearSem_ID: {Notification?.FacYearSem_ID}");

            try
            {
                var semester = await _unitOfWork.Repository<FacultyYearSemister>().GetById(Notification.FacYearSem_ID);
                if (semester is null)
                {
                    Console.WriteLine("❌ Semester not found");
                    return BadRequest(new ApiResponse(400, "Semester not found"));
                }

                var mappedNotification = new Notification
                {
                    Title = Notification.Title,
                    Massage = Notification.Massage,
                    FacYearSem_ID = Notification.FacYearSem_ID,
                };

                // 🧪 نختبر السطر هنا:
                var data = await _unitOfWork.Repository<Notification>().AddAsync(mappedNotification);

                if (data is null)
                {
                    Console.WriteLine("❌ AddAsync returned null");
                    return BadRequest(new ApiResponse(400, "Failed to add notification"));
                }

                await _unitOfWork.CompleteAsync();
                Console.WriteLine("✅ Notification added successfully");

                return Ok(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥 Exception occurred: {ex.Message}");
                return StatusCode(500, new ApiResponse(500, "Internal Server Error"));
            }
        

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
