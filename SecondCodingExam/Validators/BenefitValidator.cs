using FluentValidation;
using SecondCodingExam.Dto;

namespace SecondCodingExam.Validators
{
    public class BenefitValidator : AbstractValidator<BenefitDto>
    {
        public BenefitValidator()
        {
            RuleFor(Benefit => Benefit.GuaranteedIssue)
                .NotNull()
                .NotEmpty()
                .WithMessage(Constants.MissingGuaranteedIssue);
            RuleFor(Benefit => Benefit.MaxAgeLimit)
                .NotNull()
                .NotEmpty()
                .WithMessage(Constants.MissingMaxAgeLimit);
            RuleFor(Benefit => Benefit.MinAgeLimit)
                .NotNull()
                .NotEmpty()
                .WithMessage(Constants.MissingMinAgeLimit);
            RuleFor(Benefit => Benefit.MaxRange)
                .NotNull()
                .NotEmpty()
                .WithMessage(Constants.MissingMaxRange);
            RuleFor(Benefit => Benefit.MinRange)
                .NotNull()
                .NotEmpty()
                .WithMessage(Constants.MissingMinRange);
            RuleFor(Benefit => Benefit.Increments)
                .NotNull()
                .NotEmpty()
                .WithMessage(Constants.MissingIncrements);
        }
    }
}
