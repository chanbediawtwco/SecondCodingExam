using SecondCodingExam.Models;

namespace SecondCodingExam.Services.Interface
{
    public interface IBenefitHistoryService
    {
        public Task<BenefitsHistory> GetBenefitHistoriesById(int BenefitId);
        public Task<IAsyncEnumerable<BenefitsHistory>> GetBenefitHistories(int BenefitId, int PageNumber);
        public Task<IAsyncEnumerable<CustomersBenefitsHistory>> GetCustomersBenefitHistory(int CustomerId, int PageNumber);
        public Task<int> GetCustomersBenefitsHistoryId(int CurrentBenefitId);
        public Task DeleteBenefitHistory(int BenefitId);
        public Task DeleteCustomerBenefitHistory(int CustomerBenefitHistoryId);
        public Task MapBenefitToBenefitHistory(CustomersCurrentBenefit CurrentBenefit, string UserFullname, DateTime Timestamp);
    }
}
