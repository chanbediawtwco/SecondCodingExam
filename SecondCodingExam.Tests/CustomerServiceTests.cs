using SecondCodingExam.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondCodingExam.Tests
{
    public class CustomerServiceTests
    {
        [Fact]
        public void GetAllCustomers()
        {
            var MockCustomerService = new CustomerService(null,null,null,null,null,null,null,null,null,null);

            var Result = MockCustomerService.GetAllCustomers(1);

            Assert.NotNull(Result);
        }
    }
}
