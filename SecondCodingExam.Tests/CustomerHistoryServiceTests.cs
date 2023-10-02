using SecondCodingExam.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondCodingExam.Tests
{
    public class CustomerHistoryServiceTests
    {
        public void GetCustomerHistory()
        {
            var MockCustomerHistoryService = new CustomerHistoryService(null,null,null,null,null,null,null);

            var Result = MockCustomerHistoryService.GetCustomerHistory(1,1);

            Assert.NotNull(Result);
        }
    }
}
