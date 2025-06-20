using API.DTOs;
using Core.FingerId; // افترضنا أن هذا يحتوي على تعريف SensorData
using Microsoft.AspNetCore.Http; // قد لا تكون ضرورية إذا كان BaseApiController لا يستخدمها مباشرة
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// using Project.APIS.Erorrs; // هذا الاستيراد لم يعد موجودًا في الكود الجديد، لذلك أزيل
using Repository.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic; 

namespace API.Controllers
{
    public class SensorDataController : BaseApiController 
    {
        private readonly StoreContext _storeContext;
        public SensorDataController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        [HttpPost]
        public async Task<ActionResult<SensorData>> PostData(SensorDataDTO sensorData)
        {
            var mappedData = new SensorData
            {
                ID = sensorData.ID, 
                Timestamp = sensorData.Timestamp,  
            };

            _storeContext.SensorData.Add(mappedData);
            await _storeContext.SaveChangesAsync();

            return Ok(mappedData);
        }
        [HttpGet("{id}")] 
        public async Task<ActionResult<SensorDataDTO>> GetData(int id)
        {
            var data = await _storeContext.SensorData
                                .Where(x => x.ID == id)
                                .OrderByDescending(x => x.Timestamp)
                                .FirstOrDefaultAsync(); 

            if (data == null)
                return NotFound(); 

            
            var dto = new SensorDataDTO
            {
                ID = data.ID,
                Timestamp = data.Timestamp 
            };

            return Ok(dto);
        }
        [HttpGet("last-id")]
        public async Task<ActionResult<int>> GetLastId()
        {
            var lastId = await _storeContext.SensorData
                                .OrderByDescending(x => x.ID)
                                .Select(x => x.ID)
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
                                    ID = x.ID,
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
        [HttpGet("generate-id")]
        public async Task<ActionResult<int>> GenerateId()
        {
            var lastId = await _storeContext.SensorData
                .OrderByDescending(x => x.ID)
                .Select(x => x.ID)
                .FirstOrDefaultAsync();

            int newId = lastId + 1; 
            return Ok(newId);
        }
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetSensorDataCount() 
        {
            var count = await _storeContext.SensorData.CountAsync();
            return Ok(count);
        }
    }
}
