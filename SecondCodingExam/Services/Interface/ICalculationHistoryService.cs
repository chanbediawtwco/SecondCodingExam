using SecondCodingExam.Models;

namespace SecondCodingExam.Services.Interface
{
    public interface ICalculationHistoryService
    {
        public Task<IAsyncEnumerable<CalculationsHistory>> GetCalculationHistory(int CustomerId, int PageNumber);
    }
}
