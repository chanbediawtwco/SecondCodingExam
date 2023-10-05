using SecondCodingExam.Models;
using SecondCodingExam.Services.Interface;

namespace SecondCodingExam.Services
{
    public abstract class GenericAuditModel: AuditModifiedModel
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public abstract class GenericAuditModelForHistory: AuditModifiedModel
    {
        public string? BenefitCreatedBy { get; set; }
        public DateTime? BenefitCreatedDate { get; set; }
    }

    public abstract class AuditModifiedModel
    {
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public abstract class GenericAuditClass: GenericAuditModel
    {
        public async Task AddAuditStamp(string userFullname, DateTime timestamp, bool isModified)
        {
            if (isModified)
            {
                ModifiedBy = userFullname;
                ModifiedDate = timestamp;
            }
            else
            {
                CreatedBy = userFullname;
                CreatedDate = timestamp;
            }
        }
    }
    public abstract class GenericAuditClassForHistory : GenericAuditModelForHistory
    {
        public async Task AddAuditStamp(string userFullname, DateTime timestamp, bool isModified)
        {
            if (isModified)
            {
                ModifiedBy = userFullname;
                ModifiedDate = timestamp;
            }
            else
            {
                BenefitCreatedBy = userFullname;
                BenefitCreatedDate = timestamp;
            }
        }
    }
    public class AuditService : IAuditService
    {
        public async Task AddAuditStampToCalculation(IAsyncEnumerable<Calculation> Calculations, DateTime Timestamp)
        {
            await foreach (Calculation Calculation in Calculations)
            {
                Calculation.IsRecalculated = true;
                Calculation.ModifiedDate = Timestamp;
            }
        }
    }
}
