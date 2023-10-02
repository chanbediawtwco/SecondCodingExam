using SecondCodingExam.Models;

namespace SecondCodingExam.Services.Interface
{
    public interface ICustomerHistoryService
    {
        public Task DeleteCustomerHistory(int CustomerHistoryId);
        public Task<IAsyncEnumerable<CustomersHistory>> GetCustomerHistory(int PageNumber, int CustomerId);
        public Task MapCustomerHistoryData(Customer Customer, CustomersCurrentBenefit CurrentBenefit, DateTime Timestamp);
    }
}
