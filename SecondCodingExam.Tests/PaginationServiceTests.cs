using SecondCodingExam.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondCodingExam.Tests
{
    public class PaginationServiceTests
    {
        [Fact]
        public void HasChanges_RecordPropertiesChanged_ReturnsTrue()
        {
            var Pagination = new PaginationService();
            var Page = Pagination.GetPageNumber(1);

            Assert.Equal(0, Page);
        }
    }
}
