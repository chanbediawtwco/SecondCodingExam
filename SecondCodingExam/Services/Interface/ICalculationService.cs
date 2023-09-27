using SecondCodingExam.Models;

namespace SecondCodingExam.Services.Interface
{
    public interface ICalculationService
    {
        public Task CalculateBenefits(CustomersCurrentBenefit Benefit, Customer Customer);
        public Task<IAsyncEnumerable<Calculation>> GetCalculations(int BenefitId, int CustomerId);
        public Task<IAsyncEnumerable<Calculation>> GetCurrentCalculation(int CustomerId);
        public Task MapPreviousCalculationsToHistory(int CustomerId, DateTime Today);
    }
}
