using SecondCodingExam.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondCodingExam.Tests
{
    public class CalculationHistoryServiceTests
    {
        [Fact]
        public void GetCalculationHistory()
        {
            var MockCalculationHistoryService = new CalculationHistoryService(null,null, null);

            var Result = MockCalculationHistoryService.GetCalculationHistory(1,1);

            Assert.NotNull(Result);
        }
    }
}
