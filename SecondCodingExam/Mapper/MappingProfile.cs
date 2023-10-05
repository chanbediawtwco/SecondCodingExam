using AutoMapper;
using SecondCodingExam.Dto;
using SecondCodingExam.Models;

namespace SecondCodingExam.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<UserRegistrationDto, User>()
                .ForMember(UserRegistration => UserRegistration.Firstname,
                Option => Option.MapFrom(User => User.Firstname))
                .ForMember(UserRegistration => UserRegistration.Lastname,
                Option => Option.MapFrom(User => User.Lastname))
                .ForMember(UserRegistration => UserRegistration.Email,
                Option => Option.MapFrom(User => User.Email))
                .ForMember(UserRegistration => UserRegistration.Password,
                Option => Option.MapFrom(User => User.Password));
            CreateMap<CustomerDto, Customer>()
                .ForMember(Customer => Customer.Id,
                Option => Option.MapFrom(CustomerDto => CustomerDto.Id))
                .ForMember(Customer => Customer.Firstname,
                Option => Option.MapFrom(CustomerDto => CustomerDto.Firstname))
                .ForMember(Customer => Customer.Lastname,
                Option => Option.MapFrom(CustomerDto => CustomerDto.Lastname))
                .ForMember(Customer => Customer.BasicSalary,
                Option => Option.MapFrom(CustomerDto => CustomerDto.BasicSalary))
                .ForMember(Customer => Customer.Birthdate,
                Option => Option.MapFrom(CustomerDto => CustomerDto.Birthdate));
            CreateMap<Customer, CustomerDto>()
                .ForMember(CustomerDto => CustomerDto.Id,
                Option => Option.MapFrom(Customer => Customer.Id))
                .ForMember(Customer => Customer.BenefitId, Option => Option.Ignore())
                .ForMember(CustomerDto => CustomerDto.Firstname,
                Option => Option.MapFrom(Customer => Customer.Firstname))
                .ForMember(CustomerDto => CustomerDto.Lastname,
                Option => Option.MapFrom(Customer => Customer.Lastname))
                .ForMember(CustomerDto => CustomerDto.BasicSalary,
                Option => Option.MapFrom(Customer => Customer.BasicSalary))
                .ForMember(CustomerDto => CustomerDto.Birthdate,
                Option => Option.MapFrom(Customer => Customer.Birthdate));
            CreateMap<Calculation, CalculationsHistory>()
                .ForMember(CalculationsHistory => CalculationsHistory.Id, Option => Option.Ignore())
                .ForMember(CalculationsHistory => CalculationsHistory.CustomersCurrentBenefitsId,
                Option => Option.MapFrom(Calculation => Calculation.Id))
                .ForMember(CalculationsHistory => CalculationsHistory.CustomerId,
                Option => Option.MapFrom(Calculation => Calculation.CustomerId))
                .ForMember(CalculationsHistory => CalculationsHistory.Multiple,
                Option => Option.MapFrom(Calculation => Calculation.Multiple))
                .ForMember(CalculationsHistory => CalculationsHistory.BenefitsAmountQuotation,
                Option => Option.MapFrom(Calculation => Calculation.BenefitsAmountQuotation))
                .ForMember(CalculationsHistory => CalculationsHistory.PendedAmount,
                Option => Option.MapFrom(Calculation => Calculation.PendedAmount))
                .ForMember(CalculationsHistory => CalculationsHistory.CurrentBenefit,
                Option => Option.MapFrom(Calculation => Calculation.CurrentBenefit))
                .ForMember(CalculationsHistory => CalculationsHistory.CreatedDate,
                Option => Option.MapFrom(Calculation => Calculation.CreatedDate));
            CreateMap<Benefit, BenefitsHistory>()
                .ForMember(Benefit => Benefit.Id, Option => Option.Ignore())
                .ForMember(BenefitsHistory => BenefitsHistory.UserId,
                Option => Option.MapFrom(Benefit => Benefit.UserId))
                .ForMember(BenefitsHistory => BenefitsHistory.BenefitId,
                Option => Option.MapFrom(Benefit => Benefit.Id))
                .ForMember(BenefitsHistory => BenefitsHistory.GuaranteedIssue,
                Option => Option.MapFrom(Benefit => Benefit.GuaranteedIssue))
                .ForMember(BenefitsHistory => BenefitsHistory.MaxAgeLimit,
                Option => Option.MapFrom(Benefit => Benefit.MaxAgeLimit))
                .ForMember(BenefitsHistory => BenefitsHistory.MinAgeLimit,
                Option => Option.MapFrom(Benefit => Benefit.MinAgeLimit))
                .ForMember(BenefitsHistory => BenefitsHistory.MaxRange,
                Option => Option.MapFrom(Benefit => Benefit.MaxRange))
                .ForMember(BenefitsHistory => BenefitsHistory.MinRange,
                Option => Option.MapFrom(Benefit => Benefit.MinRange))
                .ForMember(BenefitsHistory => BenefitsHistory.Increments,
                Option => Option.MapFrom(Benefit => Benefit.Increments))
                .ForMember(BenefitsHistory => BenefitsHistory.BenefitCreatedDate,
                Option => Option.MapFrom(Benefit => Benefit.CreatedDate))
                .ForMember(BenefitsHistory => BenefitsHistory.BenefitCreatedBy,
                Option => Option.MapFrom(Benefit => Benefit.CreatedBy));
            CreateMap<BenefitDto, Benefit>()
                .ForMember(Benefit => Benefit.GuaranteedIssue,
                Option => Option.MapFrom(BenefitDto => BenefitDto.GuaranteedIssue))
                .ForMember(Benefit => Benefit.MaxAgeLimit,
                Option => Option.MapFrom(BenefitDto => BenefitDto.MaxAgeLimit))
                .ForMember(Benefit => Benefit.MinAgeLimit,
                Option => Option.MapFrom(BenefitDto => BenefitDto.MinAgeLimit))
                .ForMember(Benefit => Benefit.MaxRange,
                Option => Option.MapFrom(BenefitDto => BenefitDto.MaxRange))
                .ForMember(Benefit => Benefit.MinRange,
                Option => Option.MapFrom(BenefitDto => BenefitDto.MinRange))
                .ForMember(Benefit => Benefit.Increments,
                Option => Option.MapFrom(BenefitDto => BenefitDto.Increments));
            CreateMap<Benefit, CustomersCurrentBenefit>()
                .ForMember(CustomersCurrentBenefit => CustomersCurrentBenefit.Id, Option => Option.Ignore())
                .ForMember(CustomersCurrentBenefit => CustomersCurrentBenefit.CreatedBy, Option => Option.Ignore())
                .ForMember(CustomersCurrentBenefit => CustomersCurrentBenefit.CreatedDate, Option => Option.Ignore())
                .ForMember(CustomersCurrentBenefit => CustomersCurrentBenefit.ModifiedBy, Option => Option.Ignore())
                .ForMember(CustomersCurrentBenefit => CustomersCurrentBenefit.ModifiedDate, Option => Option.Ignore())
                .ForMember(CustomersCurrentBenefit => CustomersCurrentBenefit.BenefitId,
                Option => Option.MapFrom(Benefit => Benefit.Id));
            CreateMap<CustomersCurrentBenefit, CustomersBenefitsHistory>()
                .ForMember(CustomersBenefitsHistory => CustomersBenefitsHistory.Id, Option => Option.Ignore())
                .ForMember(BenefitsHistory => BenefitsHistory.CustomersCurrentBenefitsId,
                Option => Option.MapFrom(Benefit => Benefit.BenefitId))
                .ForMember(BenefitsHistory => BenefitsHistory.BenefitCreatedDate,
                Option => Option.MapFrom(Benefit => Benefit.CreatedDate))
                .ForMember(BenefitsHistory => BenefitsHistory.BenefitCreatedBy,
                Option => Option.MapFrom(Benefit => Benefit.CreatedBy));
            CreateMap<Customer, CustomersHistory>()
                .ForMember(CustomersHistory => CustomersHistory.Id, Option => Option.Ignore())
                .ForMember(CustomersHistory => CustomersHistory.CustomerId,
                Option => Option.MapFrom(Customer => Customer.Id));
            CreateMap<Benefit, BenefitDto>()
                .ForMember(BenefitDto => BenefitDto.Id,
                Option => Option.MapFrom(Benefit => Benefit.Id))
                .ForMember(BenefitDto => BenefitDto.GuaranteedIssue,
                Option => Option.MapFrom(Benefit => Benefit.GuaranteedIssue))
                .ForMember(BenefitDto => BenefitDto.MaxAgeLimit,
                Option => Option.MapFrom(Benefit => Benefit.MaxAgeLimit))
                .ForMember(BenefitDto => BenefitDto.MinAgeLimit,
                Option => Option.MapFrom(Benefit => Benefit.MinAgeLimit))
                .ForMember(BenefitDto => BenefitDto.MaxRange,
                Option => Option.MapFrom(Benefit => Benefit.MaxRange))
                .ForMember(BenefitDto => BenefitDto.MinRange,
                Option => Option.MapFrom(Benefit => Benefit.MinRange))
                .ForMember(BenefitDto => BenefitDto.Increments,
                Option => Option.MapFrom(Benefit => Benefit.Increments));
            CreateMap<CustomersCurrentBenefit, CustomersCurrentBenefitDto>();
            CreateMap<CustomersBenefitsHistory, CustomersBenefitsHistoryDto>()
                .ForMember(CustomersBenefitsHistoryDto => CustomersBenefitsHistoryDto.Id,
                Option => Option.MapFrom(CustomersBenefitsHistory => CustomersBenefitsHistory.Id))
                .ForMember(CustomersBenefitsHistoryDto => CustomersBenefitsHistoryDto.CustomersCurrentBenefitsId,
                Option => Option.MapFrom(CustomersBenefitsHistory => CustomersBenefitsHistory.CustomersCurrentBenefitsId))
                .ForMember(CustomersBenefitsHistoryDto => CustomersBenefitsHistoryDto.GuaranteedIssue,
                Option => Option.MapFrom(CustomersBenefitsHistory => CustomersBenefitsHistory.GuaranteedIssue))
                .ForMember(CustomersBenefitsHistoryDto => CustomersBenefitsHistoryDto.MaxAgeLimit,
                Option => Option.MapFrom(CustomersBenefitsHistory => CustomersBenefitsHistory.MaxAgeLimit))
                .ForMember(CustomersBenefitsHistoryDto => CustomersBenefitsHistoryDto.MinAgeLimit,
                Option => Option.MapFrom(CustomersBenefitsHistory => CustomersBenefitsHistory.MinAgeLimit))
                .ForMember(CustomersBenefitsHistoryDto => CustomersBenefitsHistoryDto.MaxRange,
                Option => Option.MapFrom(CustomersBenefitsHistory => CustomersBenefitsHistory.MaxRange))
                .ForMember(CustomersBenefitsHistoryDto => CustomersBenefitsHistoryDto.MinRange,
                Option => Option.MapFrom(CustomersBenefitsHistory => CustomersBenefitsHistory.MinRange))
                .ForMember(CustomersBenefitsHistoryDto => CustomersBenefitsHistoryDto.Increments,
                Option => Option.MapFrom(CustomersBenefitsHistory => CustomersBenefitsHistory.Increments));
            CreateMap<BenefitsHistory, BenefitsHistoryDto>();
        }
    }
}
