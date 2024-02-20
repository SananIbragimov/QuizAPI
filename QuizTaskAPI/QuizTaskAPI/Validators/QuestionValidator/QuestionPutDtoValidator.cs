using FluentValidation;
using QuizTaskAPI.DTOs.Question;

namespace QuizTaskAPI.Validators.QuestionValidator
{
    public class QuestionPutDtoValidator : AbstractValidator<QuestionPutDto>
    {
        public QuestionPutDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Question Name can't be empty")
                .Length(15, 300).WithMessage("Question Name must be between 15 and 300 characters");

            RuleFor(x => x.Points)
                .NotEmpty().WithMessage("Points cannot be empty")
                .GreaterThanOrEqualTo(0).WithMessage("Points must be greater than or equal to 0")
                .LessThanOrEqualTo(100).WithMessage("Points must be less than or equal to 100");
        }
    }
}
