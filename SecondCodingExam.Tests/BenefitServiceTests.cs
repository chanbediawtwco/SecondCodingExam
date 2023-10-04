using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using SecondCodingExam.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondCodingExam.Tests
{
    public class BenefitServiceTests
    {
        [Fact]
        public void GetBenefitById()
        {
            var MockBenefitService = new BenefitService(null, null, null, null, null, null, null);

            var Result = MockBenefitService.GetBenefitById(1);

            Assert.NotNull(Result);
        }
        [Fact]
        public void GetCustomerCurrentBenefit()
        {
            var MockBenefitService = new BenefitService(null, null, null, null, null, null, null);

            var Result = MockBenefitService.GetCustomerCurrentBenefit(1);

            Assert.NotNull(Result);
        }
        [Fact]
        public void GetBenefits()
        {
            var MockBenefitService = new BenefitService(null, null, null, null, null, null, null);

            var Result = MockBenefitService.GetBenefitsAsync(1);

            Assert.NotNull(Result);
        }
        [Fact]
        public void GetAllBenefits()
        {
            var MockBenefitService = new BenefitService(null, null, null, null, null, null, null);

            var Result = MockBenefitService.GetBenefitsAsync(null);

            Assert.NotNull(Result);
        }
    }
}
