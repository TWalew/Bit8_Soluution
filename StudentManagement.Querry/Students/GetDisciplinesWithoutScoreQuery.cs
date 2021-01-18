using System.Collections.Generic;

namespace StudentManagement.Services.Students
{
    public class GetDisciplinesWithoutScoreQuery
    {
        public string StudentName { get; set; }
        public IEnumerable<string> DisciplineNames { get; set; }
    }
}