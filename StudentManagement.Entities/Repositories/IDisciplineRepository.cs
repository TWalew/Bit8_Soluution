using System.Threading.Tasks;
using StudentManagement.Domain.Models;

namespace StudentManagement.Entities.Repositories
{
    public interface IDisciplineRepository
    {
        Task AddAsync(Discipline entity);
        Task UpdateAsync(Discipline entity);
        Task DeleteAsync(Discipline entity);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);

        Task<bool> HasScoresAsync(int id);
    }
}