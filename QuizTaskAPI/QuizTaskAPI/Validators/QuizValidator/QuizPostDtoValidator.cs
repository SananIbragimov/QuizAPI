using FluentValidation;
using QuizTaskAPI.DTOs.Quiz;

namespace QuizTaskAPI.Validators.QuizValidator
{
    public class QuizPostDtoValidator : AbstractValidator<QuizPostDto>
    {
        public QuizPostDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Quiz Name can't be empty")
                .Length(5, 30).WithMessage("Quiz Name must be between 5 and 30 characters");

            RuleFor(x => x.CreationDate)
                .NotEmpty().WithMessage("Creation date cannot be empty")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Creation date must be in the past");
        }
    }
}
