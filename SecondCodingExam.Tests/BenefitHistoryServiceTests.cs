using SecondCodingExam.Models;
using SecondCodingExam.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondCodingExam.Tests
{
    public class BenefitHistoryServiceTests
    {
        [Fact]
        public void GetBenefitHistoriesById()
        {
            var MockBenefitService = new BenefitHistoryService(null, null, null, null, null, null);

            var Result = MockBenefitService.GetBenefitHistoriesById(1);

            Assert.NotNull(Result);
        }
        [Fact]
        public void GetBenefitHistories()
        {
            var MockBenefitService = new BenefitHistoryService(null, null, null, null, null, null);

            var Result = MockBenefitService.GetBenefitHistories(1,1);

            Assert.NotNull(Result);
        }
        [Fact]
        public void GetCustomersBenefitHistory()
        {
            var MockBenefitService = new BenefitHistoryService(null, null, null, null, null, null);

            var Result = MockBenefitService.GetCustomersBenefitHistory(1,1);

            Assert.NotNull(Result);
        }
    }
}
