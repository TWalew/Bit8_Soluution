using System.Threading.Tasks;
using StudentManagement.Query;
using StudentManagement.Services;
using StudentManagement.Services.Semesters;
using StudentManagement.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SemesterController : Controller
    {
        private readonly ISemesterService _semesterService;
        private readonly ISemesterQuery _semesterQuery;
        
        public SemesterController(ISemesterService semesterService, ISemesterQuery semesterQuery)
        {
            _semesterService = semesterService;
            _semesterQuery = semesterQuery;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSemester(CreateSemesterRequest request)
        {
            var result = await _semesterService.CreateAsync(request);
            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSemesters()
        {
            var result = await _semesterQuery.GetAllWithDisciplinesAsync();
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSemester([FromBody] int id)
        {
            var result = await _semesterService.DeleteAsync(id);
            
            if (result.IsFailed)
            {
                return UnprocessableEntity(result.ToErrorArray());
            }

            return Ok();
        }
        
        [HttpPatch]
        public async Task<IActionResult> UpdateSemester(UpdateSemesterRequest request)
        {
            var result = await _semesterService.UpdateAsync(request);
            
            if (result.IsFailed)
            {
                return UnprocessableEntity(result.ToErrorArray());
            }

            return Ok();
        }
    }
}