using System.Collections.Generic;
using System.Threading.Tasks;
using StudentManagement.Services.Disciplines;
using FluentResults;

namespace StudentManagement.Services
{
    public interface IDisciplineService
    {
        Task<Result<int>> CreateAsync(CreateDisciplineRequest request);
        Task<Result> UpdateAsync(UpdateDisciplineRequest request);
        Task<Result> DeleteAsync(int id);
    }
}