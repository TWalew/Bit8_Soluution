using System.Collections.Generic;
using FluentValidation;

namespace StudentManagement.Services.Semesters
{
    public class UpdateSemesterRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateSemesterRequestValidator : AbstractValidator<UpdateSemesterRequest>
    {
        public UpdateSemesterRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1);
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(128);
        }
    }
}