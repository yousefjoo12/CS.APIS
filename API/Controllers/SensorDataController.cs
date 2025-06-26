using API.DTOs;
using Core;
using Core.FingerId;  
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using Repository.Data;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Controllers
{
    public class SensorDataController : BaseApiController 
    {
        private readonly StoreContext _storeContext;
        private readonly IUnitOfWork _unitOfWork;

        public SensorDataController(StoreContext storeContext,IUnitOfWork unitOfWork)
        {
            _storeContext = storeContext;
            _unitOfWork = unitOfWork;
        }
        [HttpPost("EnRoll")] 
        public async Task<ActionResult<SensorData>> EnRoll(int ID)
        {
            var mappedData = new SensorData
            {
                FingerID = ID
            };

            _storeContext.SensorData.Add(mappedData);
            await _storeContext.SaveChangesAsync();

            return Ok(mappedData);
        }
        [HttpGet("Last-id")]
        public async Task<ActionResult<int>> GetLastId()
        {
            var lastId = await _storeContext.SensorData
                                .OrderByDescending(x => x.FingerID)
                                .Select(x => x.FingerID)
                                .FirstOrDefaultAsync(); 

            return Ok(lastId);
        } 
        [HttpPost("Clear")]
        public async Task<IActionResult> ClearData()
        {
            _storeContext.SensorData.RemoveRange(_storeContext.SensorData);
            await _storeContext.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<IReadOnlyList<SensorData>>> GetAllStudets()
        {
            var query = _storeContext.SensorData.Select(x => x.FingerID);
            return Ok(query); //200

        }

    }
}
