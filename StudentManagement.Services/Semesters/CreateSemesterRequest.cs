using System.Collections.Generic;
using FluentValidation;

namespace StudentManagement.Services.Semesters
{
    public class CreateSemesterRequest
    {
        public string Name { get; set; }
        public ICollection<int> DisciplineIds { get; set; }
    }

    public class CreateSemesterRequestValidator : AbstractValidator<CreateSemesterRequest>
    {
        public CreateSemesterRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(128);
            
            RuleFor(x => x.DisciplineIds)
                .Must(x => x.Count > 0)
                    .WithMessage("Must contain at least one discipline");
        }
    }
}