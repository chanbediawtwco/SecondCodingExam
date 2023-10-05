using SecondCodingExam.Dto;
using SecondCodingExam.Models;

namespace SecondCodingExam.Services.Interface
{
    public interface ICalculationHistoryService
    {
        public Task<IAsyncEnumerable<CalculationsHistoryDto>> GetCalculationHistory(int CustomerId, int PageNumber);
    }
}
