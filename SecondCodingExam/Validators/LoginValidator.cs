using FluentValidation;
using SecondCodingExam.Dto;

namespace SecondCodingExam.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator() 
        {
            RuleFor(Login => Login.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage(Constants.MissingEmail);
            RuleFor(Login => Login.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage(Constants.MissingPassword);
        }
    }
}
