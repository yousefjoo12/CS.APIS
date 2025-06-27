using API.DTOs;
using Core;
using Core.Entities;
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
       
        [HttpGet("GetAll")]
        public async Task<ActionResult<IReadOnlyList<SensorData>>> GetAll()
        {
            var data = await _unitOfWork.Repository<SensorData>().GetAll();
            return Ok(data); //200

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SensorData>> GetById(int id)
        {
            var data = await _unitOfWork.Repository<SensorData>().GetById(id);

            return Ok(data);
        }
        [HttpPost("EnRoll")]
        public async Task<ActionResult<SensorData>> EnRoll([FromBody] SensorData model)
        { 
            var data = await _unitOfWork.Repository<SensorData>().AddAsync(model); 
            await _storeContext.SaveChangesAsync(); 
            return Ok(data);
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
      

    }
}
