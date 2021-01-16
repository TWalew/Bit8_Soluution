using System.Collections.Generic;

namespace StudentManagement.Query.Students
{
    public class GetAllWithSemestersQuery
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public IEnumerable<string> SemesterNames { get; set; }
    }
}