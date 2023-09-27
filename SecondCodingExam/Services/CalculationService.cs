using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecondCodingExam.Data;
using SecondCodingExam.Models;
using SecondCodingExam.Services.Interface;

namespace SecondCodingExam.Services
{
    public class CalculationService : ICalculationService
    {
        private readonly IMapper _mapper;
        private readonly SecondCodingExamDbContext _context;
        public CalculationService(
            IMapper mapper,
            SecondCodingExamDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IAsyncEnumerable<Calculation>> GetCalculations(int BenefitId, int CustomerId)
            => await Task.FromResult(_context.Calculations.Where(Calculation =>
                Calculation.CustomersCurrentBenefitsId == BenefitId
                && Calculation.CustomerId == CustomerId
                && Calculation.ModifiedDate == null
                && !Calculation.IsRecalculated)
                .AsAsyncEnumerable());
        public async Task<IAsyncEnumerable<Calculation>> GetCurrentCalculation(int CustomerId)
            => await Task.FromResult(_context.Calculations
                .Where(Calculation => Calculation.CustomerId == CustomerId
                && !Calculation.IsRecalculated)
            .AsAsyncEnumerable());
        public async Task MapPreviousCalculationsToHistory(int CustomerId, DateTime Today)
        {
            IAsyncEnumerable<Calculation> CurrentCalculations = await GetCurrentCalculation(CustomerId);
            await foreach (var CurrentCalculation in CurrentCalculations)
            {
                CalculationsHistory CalculationsHistory = _mapper.Map<CalculationsHistory>(CurrentCalculation);
                CalculationsHistory.ModifiedDate = Today;
                _context.CalculationsHistories.Add(CalculationsHistory);
            }
        }
        public async Task CalculateBenefits(CustomersCurrentBenefit CustomersCurrentBenefit, Customer Customer)
        {
            for (var Multiple = CustomersCurrentBenefit.MinRange; Multiple <= CustomersCurrentBenefit.MaxRange; Multiple += CustomersCurrentBenefit.Increments)
            {
                Calculation NewCalculation = new Calculation();
                await MapNewCalculation(NewCalculation, Customer, CustomersCurrentBenefit, Multiple);
                await _context.Calculations.AddAsync(NewCalculation);
            }
            await _context.SaveChangesAsync();
        }
        private async Task MapNewCalculation(Calculation NewCalculation, Customer Customer, CustomersCurrentBenefit Benefit, int Multiple)
        {
            NewCalculation.Multiple = Multiple;
            NewCalculation.CustomersCurrentBenefitsId = Benefit.Id;
            NewCalculation.CustomerId = Customer.Id;
            NewCalculation.CreatedDate = Customer.CreatedDate;
            NewCalculation.BenefitsAmountQuotation = await CalculateBenefitsAmountQuotation(NewCalculation, Customer);
            NewCalculation.PendedAmount = await IsEligible(Benefit, Customer, NewCalculation) ? await CalculatePendedAmount(NewCalculation, Benefit) : 0;
            NewCalculation.CurrentBenefit = await IsEligible(Benefit, Customer, NewCalculation) ? Constants.ForApproval : (await CalculatePendedAmount(NewCalculation, Benefit)).ToString();
        }
        private async Task<bool> IsEligible(CustomersCurrentBenefit Benefit, Customer Customer, Calculation NewCalculation)
            => await Task.FromResult(await IsAgeWithinAgeLimit(await GetCustomersAge(Customer, Customer.CreatedDate), Benefit) && await CalculatePendedAmount(NewCalculation, Benefit) > Benefit.GuaranteedIssue);
        private async Task<bool> IsAgeWithinAgeLimit(int CustomersAge, CustomersCurrentBenefit Benefit)
            => await Task.FromResult(Benefit.MinAgeLimit <= CustomersAge && CustomersAge <= Benefit.MaxAgeLimit);
        private async Task<int> ConvertDaysToYears(int DaysCount)
        {
            return await Task.FromResult(DaysCount / 365);
        }
        private async Task<int> CalculatePendedAmount(Calculation NewCalculation, CustomersCurrentBenefit Benefit)
        {
            return await Task.FromResult(NewCalculation.BenefitsAmountQuotation - Benefit.GuaranteedIssue);
        }
        private async Task<int> CalculateBenefitsAmountQuotation(Calculation NewCalculation, Customer Customer)
        {
            return await Task.FromResult(Customer.BasicSalary * NewCalculation.Multiple);
        }
        private async Task<int> CountDaysBetweenTwoDates(DateTime Date1, DateTime Date2)
        {
            Double DateDifference = (Date1 - Date2).TotalDays;
            return await Task.FromResult(Convert.ToInt32(Math.Floor(DateDifference)));
        }
        private async Task<int> GetCustomersAge(Customer Customer, DateTime TimeStamp)
        {
            int DaysDifference = await CountDaysBetweenTwoDates(TimeStamp, Customer.Birthdate);
            // Need to less leap year as the leap year may affect the birthdate
            // Ex. 12/11/2012 => 12/13/2018 (2 Leap year passed. Remove the 2 additional days)
            int LeapYears = await LeapYearCountBetweenTwoDates(TimeStamp, Customer.Birthdate);
            int Age = await ConvertDaysToYears(DaysDifference - LeapYears);
            return await Task.FromResult(Age);
        }
        private async Task<int> LeapYearCountBetweenTwoDates(DateTime Date1, DateTime Date2)
        {
            int LeapYearCount = 0;
            for (var Year = Date2.Year; Year <= Date1.Year; Year++)
            {
                if (Year % 400 == 0 || (Year % 4 == 0 && Year % 100 != 0))
                {
                    LeapYearCount++;
                }
            }
            return await Task.FromResult(LeapYearCount);
        }
    }
}
