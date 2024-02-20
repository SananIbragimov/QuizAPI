using FluentValidation;
using QuizTaskAPI.DTOs.Option;

namespace QuizTaskAPI.Validators.OptionValidator
{
    public class OptionPutDtoValidator : AbstractValidator<OptionPutDto>
    {
        public OptionPutDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Option Name can't be empty")
                .Length(5, 100).WithMessage("Option Name must be between 5 and 100 characters");

            RuleFor(x => x.IsCorrect)
                .NotEmpty().WithMessage("IsCorrect cannot be empty");
        }
    }
}
