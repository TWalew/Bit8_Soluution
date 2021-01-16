using FluentValidation;

namespace StudentManagement.Services.Disciplines
{
    public class UpdateDisciplineRequest
    {
        public int Id { get; set; }
        public string ProfessorName { get; set; }
        public string Name { get; set; }
    }

    public class UpdateDisciplineRequestValidator : AbstractValidator<UpdateDisciplineRequest>
    {
        public UpdateDisciplineRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1);
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(128);

            RuleFor(x => x.ProfessorName)
                .NotEmpty()
                .MaximumLength(128);
        }
    }
}