using System.Collections.Generic;
using System.Threading.Tasks;
using StudentManagement.Domain.Models;

namespace StudentManagement.Entities.Repositories
{
    public interface IStudentRepository
    {
        Task AddAsync(Student entity);
        Task DeleteAsync(Student entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(Student entity);
        Task<Student> GetAsync(int id);
        Task AddRelationToSemesterAsync(int studentId, int semesterId);
        Task RemoveRelationToSemesterAsync(int studentId, int semesterId);
        Task<bool> ExistsAsync(int id);
    }
}