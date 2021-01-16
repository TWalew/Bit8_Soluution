using System.Collections.Generic;

namespace StudentManagement.Query.Semesters
{
    public class GetAllWithDisciplinesQuery
    {
        public int Id { get; set; }
        public string SemesterName { get; set; }
        public IEnumerable<string> DisciplineNames { get; set; }
    }
}