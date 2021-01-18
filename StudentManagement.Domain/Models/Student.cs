using StudentManagement.Common;

namespace StudentManagement.Domain.Models
{
    public class Student : DatabaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SemesterId { get; set; }

        public void AssignToSemester(int semesterId)
        {
            SemesterId = semesterId;
        }
    }
}