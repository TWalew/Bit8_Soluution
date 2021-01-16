using System.Collections.Generic;
using System.Threading.Tasks;
using StudentManagement.Query.Semesters;

namespace StudentManagement.Query
{
    public interface ISemesterQuery
    {
        Task<IEnumerable<GetAllWithDisciplinesQuery>> GetAllWithDisciplinesAsync();
    }
}