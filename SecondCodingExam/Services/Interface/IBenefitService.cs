using SecondCodingExam.Dto;
using SecondCodingExam.Models;

namespace SecondCodingExam.Services.Interface
{
    public interface IBenefitService
    {
        public Task<Benefit> GetBenefitById(int BenefitId);
        public Task<CustomersCurrentBenefit?> GetCustomerCurrentBenefit(int CustomerId);
        public Task<IAsyncEnumerable<Benefit>> GetBenefits(int PageNumber);
        public Task<IAsyncEnumerable<Benefit>> GetAllBenefits();
        public Task DeleteBenefit(int BenefitId);
        public Task SaveBenefit(BenefitDto NewBenefit);
        public Task UpdateBenefit(BenefitDto UpdatedBenefit);
        public Task SaveCurrentBenefitChanges(CustomersCurrentBenefit CustomersCurrentBenefit, DateTime Timestamp, int? CustomerId = null);
    }
}