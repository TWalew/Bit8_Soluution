using System.Threading.Tasks;
using StudentManagement.Services.Semesters;
using FluentResults;

namespace StudentManagement.Services
{
    public interface ISemesterService
    {
        Task<Result<int>> CreateAsync(CreateSemesterRequest request);
        Task<Result> UpdateAsync(UpdateSemesterRequest request);
        Task<Result> DeleteAsync(int id);
    }
}