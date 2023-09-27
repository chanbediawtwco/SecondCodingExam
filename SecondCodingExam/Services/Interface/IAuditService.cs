using SecondCodingExam.Models;

namespace SecondCodingExam.Services.Interface
{
    public interface IAuditService
    {
        public Task AddAuditStamp(Object Model, string UserFullname, DateTime Timestamp, bool IsModified);
        public Task AddAuditStampToCalculation(IAsyncEnumerable<Calculation> Calculations, DateTime Timestamp);
    }
}
