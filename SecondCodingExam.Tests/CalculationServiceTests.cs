using Moq;
using SecondCodingExam.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondCodingExam.Tests
{
    public class CalculationServiceTests
    {
        [Fact]
        public void GetCalculations()
        {
            var MockCalculationService = new CalculationService(null,null);
            var Result = MockCalculationService.GetCalculations(1, 1);
            Assert.NotNull(Result);
        }
        [Fact]
        public void GetCurrentCalculation()
        {
            var MockCalculationService = new CalculationService(null, null);
            var Result = MockCalculationService.GetCurrentCalculation(1);
            Assert.NotNull(Result);
        }
    }
}
