using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentManagement.Common;
using Dapper;
using System;

namespace StudentManagement.Query.Semesters
{
    public class SemesterQuery : QueryBase, ISemesterQuery
    {
        public SemesterQuery(IBConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<GetAllWithDisciplinesQuery>> GetAllWithDisciplinesAsync()
        {
            var sql = @"select s.id, s.name SemesterName, s.startDate StartDate, s.endDate EndDate, (SELECT group_concat(d.name)) as Discipline from semester s
							join discipline_semester ds on s.id = ds.semester_id
							join discipline d on ds.discipline_id = d.id
                            GROUP BY s.id";

            var result = await Connection.QueryAsync(sql,
                (int id, string name, DateTime startDate, DateTime endDate, string discipline) 
                => new {id, name, startDate, endDate, discipline}, 
                splitOn: "SemesterName,StartDate,EndDate,Discipline");
            
            return result.GroupBy(x => new  { x.id, x.name, x.startDate, x.endDate },
                (key, group) => new GetAllWithDisciplinesQuery
                {
                    Id = key.id,
                    SemesterName = key.name,
                    StartDate = key.startDate,
                    EndDate = key.endDate,
                    DisciplineNames = group.Select(x => x.discipline)
                });
        }
    }
}