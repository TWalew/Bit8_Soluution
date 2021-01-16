using FluentValidation;

namespace StudentManagement.Services.Disciplines
{
    public class CreateDisciplineRequest
    {
        public string ProfessorName { get; set; }
        public string Name { get; set; }
    }

    public class CreateDisciplineRequestValidator : AbstractValidator<CreateDisciplineRequest>
    {
        public CreateDisciplineRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(128);

            RuleFor(x => x.ProfessorName)
                .NotEmpty()
                .MaximumLength(128);
        }
    }
}