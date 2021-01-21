using FluentValidation;
using System.Collections.Generic;

namespace StudentManagement.Services.Students
{
    public class UpdateStudentRequest
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public bool addOrRemove { get; set; }
        public int SemesterId { get; set; }
        public ICollection<int> SemesterIds { get; set; }
    }
    
    public class UpdateStudentRequestValidator : AbstractValidator<UpdateStudentRequest>
    {
        public UpdateStudentRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1);
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(128);
        }
    }
}