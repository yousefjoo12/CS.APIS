using Core.Entities;
using Core.Repositories.Contract;
using Core.Specifications.studetsSpecifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.APIS.Erorrs; 

namespace API.Controllers
{
    public class StudetsController : BaseApiController
    {
        private readonly IGenericRepositories<Students> _studetsRepo;

        public StudetsController(IGenericRepositories<Students> StudetsRepo)
        {
            _studetsRepo = StudetsRepo;
        }
        [ProducesResponseType(typeof(Students), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]//  /Studets
        public async Task<ActionResult> GetAllStudets()
        {
            var Spec = new studetsWithSubjectSpecifications(); 
            var Studets = await _studetsRepo.GetAllWithSpecAsync(Spec);
            return Ok(Studets); //200
        }
        [ProducesResponseType(typeof(Students), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Students>> GetStudet(int id)
        {
            var Spec = new studetsWithSubjectSpecifications(id); 
            var Studet = await _studetsRepo.GetWithspecAsync(Spec);
            if (Studet == null)
            {
                return NotFound(new ApiResponse(404));// 404
            }
            return Ok(Studet); // 200
        }

    }
}
