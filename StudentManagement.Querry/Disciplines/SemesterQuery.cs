using System.Collections.Generic;
using System.Threading.Tasks;
using StudentManagement.Common;
using StudentManagement.Services.Disciplines;
using Dapper;

namespace StudentManagement.Query.Disciplines
{
    public class DisciplineQuery : QueryBase, IDisciplineQuery
    {
        public DisciplineQuery(IBConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<GetAllQuery>> GetAllAsync()
        {
            var sql = @"select * from discipline";
            var result = await Connection.QueryAsync<GetAllQuery>(sql);

            return result;
        }
    }
}