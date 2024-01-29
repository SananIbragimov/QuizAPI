using FluentValidation;
using QuizTaskAPI.DTOs.Account;

namespace QuizTaskAPI.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty();
            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
