using FluentValidation;
using QuizTaskAPI.DTOs.Quiz;

namespace QuizTaskAPI.Validators.QuizValidator
{
    public class QuizPutDtoValidator : AbstractValidator<QuizPutDto>
    {
        public QuizPutDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Quiz Name can't be empty")
                .Length(5, 30).WithMessage("Quiz Name must be between 5 and 30 characters");
        }
    }
}
