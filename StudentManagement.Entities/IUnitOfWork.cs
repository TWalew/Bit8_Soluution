using System;
using StudentManagement.Entities.Repositories;

namespace StudentManagement.Entities
{
    public interface IUnitOfWork : IDisposable
    {
        IDisciplineRepository DisciplineRepository { get; }
        ISemesterRepository SemesterRepository { get; }
        IStudentRepository StudentRepository { get; }

        void Commit();
    }
}