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
        [HttpPost]
        public async Task<ActionResult<SensorData>> PostData(SensorDataDTO sensorData)
        {
            var mappedData = new SensorData
            {
                FingerID = sensorData.FingerID, 
                Timestamp = sensorData.Timestamp 
            };

            _storeContext.SensorData.Add(mappedData);
            await _storeContext.SaveChangesAsync();

            return Ok(mappedData);
        }
        [HttpGet("GetAllByFingerID")]
        public async Task<ActionResult<SensorDataDTOCustm>> GetAllByFingerID(int id)
        {
            var data = _storeContext.SensorData.Where(x => x.FingerID == id);
            var result = new List<SensorDataDTOCustm>();

            foreach (var item in data)
            {
                var mappedData = new SensorDataDTOCustm
                {
                    FingerID = item.FingerID 
                };
                result.Add(mappedData);
            }

            return Ok(result);
        }
        [HttpGet("{id}")] 
        public async Task<ActionResult<SensorDataDTO>> GetData(int id)
        {
            var data = await _storeContext.SensorData
                                .Where(x => x.FingerID == id)
                                .OrderByDescending(x => x.Timestamp)
                                .FirstOrDefaultAsync(); 

            if (data == null)
                return NotFound(); 

            
            var dto = new SensorDataDTO
            {
                FingerID = data.FingerID,
                Timestamp = data.Timestamp 
            };

            return Ok(dto);
        }
        [HttpGet("last-id")]
        public async Task<ActionResult<int>> GetLastId()
        {
            var lastId = await _storeContext.SensorData
                                .OrderByDescending(x => x.FingerID)
                                .Select(x => x.FingerID)
                                .FirstOrDefaultAsync(); 

            return Ok(lastId);
        }
        [HttpGet]
        public async Task<ActionResult<List<SensorDataDTO>>> GetAllData()
        {
            var dataList = await _storeContext.SensorData
                                .OrderByDescending(x => x.Timestamp) 
                                .Select(x => new SensorDataDTO 
                                {
                                    FingerID = x.FingerID,
                                    Timestamp = x.Timestamp 
                                })
                                .ToListAsync(); 

            return Ok(dataList);
        }
        [HttpPost("clear")]
        public async Task<IActionResult> ClearData()
        {
            _storeContext.SensorData.RemoveRange(_storeContext.SensorData);
            await _storeContext.SaveChangesAsync();
            return Ok();
        } 
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetSensorDataCount() 
        {
            var count = await _storeContext.SensorData.CountAsync();
            return Ok(count);
        }
    }
}
