using StudentManagement.Common;
using System;

namespace StudentManagement.Domain.Models
{
    public class Semester : DatabaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}