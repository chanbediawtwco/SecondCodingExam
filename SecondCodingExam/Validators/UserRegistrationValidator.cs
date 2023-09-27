using FluentValidation;
using SecondCodingExam.Dto;

namespace SecondCodingExam.Validators
{
    public class UserRegistrationValidator : AbstractValidator<UserRegistrationDto>
    {
        public UserRegistrationValidator() 
        { 
            RuleFor(UserRegistration => UserRegistration.Firstname)
                .NotNull()
                .NotEmpty()
                .WithMessage(Constants.MissingFirstname);
            RuleFor(UserRegistration => UserRegistration.Lastname)
                .NotNull()
                .NotEmpty()
                .WithMessage(Constants.MissingLastname);
            RuleFor(UserRegistration => UserRegistration.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage(Constants.MissingEmail);
            RuleFor(UserRegistration => UserRegistration.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage(Constants.MissingPassword);
        }
    }
}
