using System.Collections.Generic;
using System.Threading.Tasks;
using StudentManagement.Query.Students;
using StudentManagement.Services.Students;

namespace StudentManagement.Query
{
    public interface IStudentQuery
    {
        Task<IEnumerable<GetTopTenStudentsQuery>> GetTopTenAsync();
        Task<IEnumerable<GetDisciplinesWithoutScoreQuery>> GetDisciplinesWithoutScore();
        Task<IEnumerable<GetAllWithSemestersQuery>> GetAllWithSemestersAsync();
    }
}