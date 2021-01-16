using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using StudentManagement.Domain.Models;
using Dapper;

namespace StudentManagement.Entities.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IDbTransaction _transaction;
        
        public StudentRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public async Task AddAsync(Student entity)
        {
            entity.Id = await _transaction.Connection.ExecuteScalarAsync<int>(
                "insert into student(name) values(@Name); select LAST_INSERT_ID()",
                new {Name = entity.Name},
                _transaction);
        }

        public async Task DeleteAsync(Student entity)
        {
            await DeleteAsync(entity.Id);
        }

        public async Task DeleteAsync(int id)
        {
            await _transaction.Connection.ExecuteAsync(
                "delete from student where id = @Id", 
                new {Id = id}
            );
        }

        public async Task UpdateAsync(Student entity)
        {
            var sql = @"update student set name=@Name, semester_id=@SemesterId where id=@Id";
            await _transaction.Connection.ExecuteAsync(sql, entity);
        }

        public async Task<Student> GetAsync(int id)
        {
            var sql = @"select * from student where id=@Id";
            return await _transaction.Connection.QuerySingleAsync<Student>(sql, new {Id = id});
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var sql = @"select count(1) from student where id=@Id";
            return await _transaction.Connection.ExecuteScalarAsync<bool>(sql, new { Id = id});
        }
    }
}