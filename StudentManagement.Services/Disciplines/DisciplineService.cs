using System;
using System.Threading.Tasks;
using StudentManagement.Domain.Models;
using StudentManagement.Entities;
using FluentResults;

 
namespace StudentManagement.Services.Disciplines
{
    public class DisciplineService : IDisciplineService
    {
        private readonly IUnitOfWork _uow;
        
        public DisciplineService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        
        public async Task<Result<int>> CreateAsync(CreateDisciplineRequest request)
        {
            var discipline = new Discipline {Name = request.Name, ProfessorName = request.ProfessorName};
            
            await _uow.DisciplineRepository.AddAsync(discipline);
            _uow.Commit();

            return Result.Ok(discipline.Id);
        }

        public async Task<Result> UpdateAsync(UpdateDisciplineRequest request)
        {
            var discipline = new Discipline
            {
                Name = request.Name, 
                ProfessorName = request.ProfessorName,
                Id = request.Id
            };

            if (!await _uow.DisciplineRepository.ExistsAsync(request.Id))
            {
                return Result.Fail("Discipline with given Id does not exist");
            }

            await _uow.DisciplineRepository.UpdateAsync(discipline);
            _uow.Commit();

            return Result.Ok();
        }

        public async Task<Result> DeleteAsync(int id)
        {
            if (id == 0)
            {
                return Result.Fail("Id must be provided");
            }
            
            var hasAssignedScores = await _uow.DisciplineRepository.HasScoresAsync(id);

            if (hasAssignedScores)
            {
                return Result.Fail("Discipline has scores assigned");
            }
                
            await _uow.DisciplineRepository.DeleteAsync(id);
            _uow.Commit();

            return Result.Ok();
        }
    }
}