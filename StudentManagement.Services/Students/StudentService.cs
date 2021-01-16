using System.Threading.Tasks;
using StudentManagement.Domain.Models;
using StudentManagement.Entities;
using FluentResults;

namespace StudentManagement.Services.Students
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _uow;
        
        public StudentService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        
        public async Task<Result<int>> CreateAsync(CreateStudentRequest request)
        {
            var student = new Student { Name = request.Name };
            
            await _uow.StudentRepository.AddAsync(student);
            _uow.Commit();

            return Result.Ok(student.Id);
        }

        public async Task<Result> AssignToSemesterAsync(AssignToSemesterRequest request)
        {
            var result = Result.Ok();

            if (!await _uow.SemesterRepository.ExistsAsync(request.SemesterId))
            {
                result.WithError("Semester with given id does not exist");
            }
            
            if (!await _uow.StudentRepository.ExistsAsync(request.StudentId))
            {
                result.WithError("Student with given id does not exist");
            }

            if (result.IsFailed) 
                return result;

            var student = await _uow.StudentRepository.GetAsync(request.StudentId);
            
            student.AssignToSemester(request.SemesterId);
            await _uow.StudentRepository.UpdateAsync(student);
            
            _uow.Commit();
            return result;
        }

        public async Task<Result> UpdateAsync(UpdateStudentRequest request)
        {
            var result = Result.Ok();
            
            if (!await _uow.StudentRepository.ExistsAsync(request.Id))
            {
                result.WithError("Student with given id does not exist");
            }

            if (result.IsFailed) 
                return result;

            await _uow.StudentRepository.UpdateAsync(new Student {Id = request.Id, Name = request.Name});
            _uow.Commit();
            return result;
        }
    }
}