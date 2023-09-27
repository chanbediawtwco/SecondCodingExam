using SecondCodingExam.Dto;
using SecondCodingExam.Models;

namespace SecondCodingExam.Services.Interface
{
    public interface ICustomerService
    {
        public Task DeleteCustomer(int CustomerId);
        public Task AddNewCustomer(CustomerDto NewCustomer);
        public Task UpdateCustomer(CustomerDto NewCustomerInformation);
        public Task<IAsyncEnumerable<Customer>> GetAllCustomers(int PageNumber);
    }
}
