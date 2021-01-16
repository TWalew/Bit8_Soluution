using System.Data;
using StudentManagement.Common;
using StudentManagement.Entities.Repositories;
using MySql.Data.MySqlClient;

namespace StudentManagement.Entities
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        private IDisciplineRepository _disciplineRepository;
        private ISemesterRepository _semesterRepository;
        private IStudentRepository _studentRepository;

        public UnitOfWork(IBConfiguration configuration)
        {
            _connection = new MySqlConnection(configuration.ConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
        
        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }

        public IDisciplineRepository DisciplineRepository => _disciplineRepository ?? (_disciplineRepository = new DisciplineRepository(_transaction));
        public ISemesterRepository SemesterRepository => _semesterRepository ?? (_semesterRepository = new SemesterRepository(_transaction));
        public IStudentRepository StudentRepository => _studentRepository ?? (_studentRepository = new StudentRepository(_transaction));
        
        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
            }
        }
    }
}