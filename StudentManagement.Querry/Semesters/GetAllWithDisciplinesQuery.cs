using System;
using System.Collections.Generic;

namespace StudentManagement.Query.Semesters
{
    public class GetAllWithDisciplinesQuery
    {
        public int Id { get; set; }
        public string SemesterName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<string> DisciplineNames { get; set; }
    }
}