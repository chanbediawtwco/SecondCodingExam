using FluentValidation;
using SecondCodingExam.Dto;

namespace SecondCodingExam.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        public CustomerValidator()
        {
            RuleFor(Customer => Customer.BenefitId)
                .NotNull()
                .NotEmpty()
                .WithMessage(Constants.MissingBenefit);
            RuleFor(Customer => Customer.Firstname)
                .NotNull()
                .NotEmpty()
                .WithMessage(Constants.MissingFirstname);
            RuleFor(Customer => Customer.Lastname)
                .NotNull()
                .NotEmpty()
                .WithMessage(Constants.MissingLastname);
            RuleFor(Customer => Customer.BasicSalary)
                .NotNull()
                .NotEmpty()
                .WithMessage(Constants.MissingSalary);
            RuleFor(Customer => Customer.Birthdate)
                .NotNull()
                .NotEmpty()
                .WithMessage(Constants.MissingBirthdate);
        }
    }
}
