using System.Threading.Tasks;
using StudentManagement.Query;
using StudentManagement.Services;
using StudentManagement.Services.Disciplines;
using StudentManagement.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinesController : ControllerBase
    {
        private readonly IDisciplineService _disciplineService;
        private readonly IDisciplineQuery _disciplineQuery;
        
        public DisciplinesController(IDisciplineService disciplineService, IDisciplineQuery disciplineQuery)
        {
            _disciplineService = disciplineService;
            _disciplineQuery = disciplineQuery;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscipline(CreateDisciplineRequest request)
        {
            var result = await _disciplineService.CreateAsync(request);
            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDisciplines()
        {
            var result = await _disciplineQuery.GetAllAsync();
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDisciplines([FromBody] int id)
        {
            var result = await _disciplineService.DeleteAsync(id);
            
            if (result.IsFailed)
            {
                return UnprocessableEntity(result.ToErrorArray());
            }

            return Ok();
        }
        
        [HttpPatch]
        public async Task<IActionResult> UpdateDiscipline(UpdateDisciplineRequest request)
        {
            var result = await _disciplineService.UpdateAsync(request);
            
            if (result.IsFailed)
            {
                return UnprocessableEntity(result.ToErrorArray());
            }

            return Ok();
        }
    }
}