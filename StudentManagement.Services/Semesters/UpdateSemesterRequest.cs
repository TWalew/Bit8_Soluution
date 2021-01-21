using System;
using System.Collections.Generic;
using FluentValidation;

namespace StudentManagement.Services.Semesters
{
    public class UpdateSemesterRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool addOrRemove { get; set; }
        public ICollection<int> DisciplineIds { get; set; }
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

            RuleFor(x => x.DisciplineIds)
                .Must(x => x.Count >= 0)
                .WithMessage("Must contain at least one discipline");
        }
    }
}