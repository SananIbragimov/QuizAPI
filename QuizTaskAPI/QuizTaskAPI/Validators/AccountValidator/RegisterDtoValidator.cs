using FluentValidation;
using QuizTaskAPI.DTOs.Account;

namespace QuizTaskAPI.Validators.AccountValidator
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator() {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName can't be empty")
                .Length(5, 25).WithMessage("FirstName must be between 5 and 25 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName can't be empty")
                .Length(5, 25).WithMessage("LastName must be between 5 and 25 characters");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username can't be empty")
                .Length(5, 25).WithMessage("Username must be between 5 and 25 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email can't be empty")
                .MinimumLength(12).WithMessage("Email can't be less than 12 characters")
                .EmailAddress().WithMessage("Email format is invalid!");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password can't be empty")
                .MinimumLength(8).WithMessage("Password can't be less than 8 characters")
                .MaximumLength(26).WithMessage("Password can't be greater than 26 characters"); ;

        }
    }
}
