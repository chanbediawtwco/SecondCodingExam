using SecondCodingExam.Dto;
using SecondCodingExam.Models;

namespace SecondCodingExam.Services.Interface
{
    public interface ICustomerService
    {
        public Task DeleteCustomer(int CustomerId);
        public Task DeleteCustomerHistory(int CustomerHistoryId);
        public Task AddNewCustomer(CustomerDto NewCustomer);
        public Task UpdateCustomer(CustomerDto NewCustomerInformation);
        public Task<IAsyncEnumerable<Customer>> GetAllCustomers(int PageNumber);
        public Task<IAsyncEnumerable<CustomersHistory>> GetCustomerHistory(int PageNumber, int CustomerId);
    }
}
