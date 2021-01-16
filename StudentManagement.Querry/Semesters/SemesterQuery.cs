using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentManagement.Common;
using Dapper;

namespace StudentManagement.Query.Semesters
{
    public class SemesterQuery : QueryBase, ISemesterQuery
    {
        public SemesterQuery(IBConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<GetAllWithDisciplinesQuery>> GetAllWithDisciplinesAsync()
        {
            var sql = @"select s.id, s.name semester, d.name discipline from semester s
                            join discipline_semester ds on s.id = ds.id
                            join discipline d on ds.discipline_id = d.id;";

            var result = await Connection.QueryAsync(sql,
                (int id, string semester, string discipline) => new {id, semester, discipline}, 
                splitOn: "semester,discipline");
            
            return result.GroupBy(x => new  { x.id, x.semester },
                (key, group) => new GetAllWithDisciplinesQuery
                {
                    Id = key.id,
                    SemesterName = key.semester, 
                    DisciplineNames = group.Select(x => x.discipline)
                });
        }
    }
}