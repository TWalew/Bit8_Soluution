using System.Collections.Generic;

namespace StudentManagement.Services.Students
{
    public class GetDisciplinesWithoutScoreQuery
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public IEnumerable<string> DisciplineNames { get; set; }
    }
}