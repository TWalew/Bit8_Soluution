using FluentValidation;

namespace StudentManagement.Services.Students
{
    public class AssignToSemesterRequest
    {
        public int StudentId { get; set; }
        public int SemesterId { get; set; }
    }

    public class AssignToSemesterRequestValidator : AbstractValidator<AssignToSemesterRequest>
    {
        public AssignToSemesterRequestValidator()
        {
            RuleFor(x => x.SemesterId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.StudentId)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}