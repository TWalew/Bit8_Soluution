using System.Collections.Generic;
using System.Threading.Tasks;
using StudentManagement.Services.Disciplines;

namespace StudentManagement.Query
{
    public interface IDisciplineQuery
    {
        Task<IEnumerable<GetAllQuery>> GetAllAsync();
    }
}