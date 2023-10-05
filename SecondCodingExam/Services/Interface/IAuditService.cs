using SecondCodingExam.Models;

namespace SecondCodingExam.Services.Interface
{
    public interface IAuditService
    {
        public Task AddAuditStampToCalculation(IAsyncEnumerable<Calculation> Calculations, DateTime Timestamp);
    }
}
