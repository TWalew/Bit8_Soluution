using StudentManagement.Common;

namespace StudentManagement.Domain.Models
{
    public class Discipline : DatabaseEntity
    {
        public string ProfessorName { get; set; }
        public string Name { get; set; }
    }
}