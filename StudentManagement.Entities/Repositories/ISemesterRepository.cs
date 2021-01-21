using System.Collections.Generic;
using System.Threading.Tasks;
using StudentManagement.Domain.Models;

namespace StudentManagement.Entities.Repositories
{
    public interface ISemesterRepository
    {
        Task AddAsync(Semester entity);
        Task UpdateAsync(Semester entity);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        
        Task AddRelationToDisciplineAsync(int semesterId, int disciplineId);

        Task RemoveRelationToDisciplineAsync(int semesterId, int disciplineId);

        Task<bool> HasStudentsAsync(int id);
    }
}