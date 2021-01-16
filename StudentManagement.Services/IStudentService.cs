using System.Collections.Generic;
using System.Threading.Tasks;
using StudentManagement.Services.Students;
using FluentResults;

namespace StudentManagement.Services
{
    public interface IStudentService
    {
        Task<Result<int>> CreateAsync(CreateStudentRequest request);
        Task<Result> AssignToSemesterAsync(AssignToSemesterRequest request);
        Task<Result> UpdateAsync(UpdateStudentRequest request);
    }
}