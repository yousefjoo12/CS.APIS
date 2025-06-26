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
        public async Task<ActionResult<SensorData>> EnRoll([FromBody] SensorDataDTO model)
        {
            var mappedData = new SensorData
            {
                ID = model.FingerID,
                Tamplate = model.Tamplate
            };

            _storeContext.SensorData.Add(mappedData);
            await _storeContext.SaveChangesAsync();

            return Ok(mappedData);
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<int>> GetById()
        {
            var lastId = await _storeContext.SensorData
                                .Select(x => x.ID)
                                .FirstOrDefaultAsync();

            return Ok(lastId);
        }
        [HttpGet("Last-id")]
        public async Task<ActionResult<int>> GetLastId()
        {
            var lastId = await _storeContext.SensorData
                                .OrderByDescending(x => x.ID)
                                .Select(x => x.ID)
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
        public async Task<ActionResult<IReadOnlyList<SensorData>>> GetAll()
        {
            var query = _storeContext.SensorData.Select(x => x.ID);
            return Ok(query); //200

        }

    }
}
