using System.Threading.Tasks;
using StudentManagement.Query;
using StudentManagement.Services;
using StudentManagement.Services.Students;
using StudentManagement.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IStudentQuery _studentQuery;
        
        public StudentsController(IStudentService studentService, IStudentQuery studentQuery)
        {
            _studentService = studentService;
            _studentQuery = studentQuery;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentRequest request)
        {
            var result = await _studentService.CreateAsync(request);
            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudentsWithSemesters()
        {
            var result = await _studentQuery.GetAllWithSemestersAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route("semesters")]
        public async Task<IActionResult> AssignToSemester(AssignToSemesterRequest request)
        {
            var result = await _studentService.AssignToSemesterAsync(request);
            
            if (result.IsFailed)
            {
                return UnprocessableEntity(result.ToErrorArray());
            }

            return Ok();
        }

        [HttpGet]
        [Route("sets/top-ten")]
        public async Task<IActionResult> GetTopTenStudents()
        {
            var result = await _studentQuery.GetTopTenAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("sets/disciplines-without-score")]
        public async Task<IActionResult> GetDisciplinesWithoutScore()
        {
            var result = await _studentQuery.GetDisciplinesWithoutScore();
            return Ok(result);
        }
        
        [HttpPatch]
        public async Task<IActionResult> UpdateStudent(UpdateStudentRequest request)
        {
            var result = await _studentService.UpdateAsync(request);
            
            if (result.IsFailed)
            {
                return UnprocessableEntity(result.ToErrorArray());
            }

            return Ok();
        }
    }
}