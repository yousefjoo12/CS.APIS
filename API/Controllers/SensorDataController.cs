using Core.FingerId;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.APIS.Erorrs;
using Repository.Data;

namespace API.Controllers
{ 
    public class SensorDataController : BaseApiController
    {
        private readonly StoreContext _storeContext;

        public SensorDataController(StoreContext storeContext) {
            _storeContext = storeContext;
        }

        [HttpPost]
        public async Task<ActionResult<SensorData>> PostData([FromBody] SensorData data)
        {
            _storeContext.SensorData.Add(data);
            await _storeContext.SaveChangesAsync();
            return Ok(new ApiResponse(200));// 200
            
        }
    }
}
