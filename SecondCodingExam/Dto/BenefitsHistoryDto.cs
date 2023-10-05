namespace SecondCodingExam.Dto
{
    public class BenefitsHistoryDto
    {
        public int? Id { get; set; }

        public int? UserId { get; set; }

        public int? BenefitId { get; set; }

        public int? GuaranteedIssue { get; set; }

        public int? MaxAgeLimit { get; set; }

        public int? MinAgeLimit { get; set; }

        public int? MaxRange { get; set; }

        public int? MinRange { get; set; }

        public int? Increments { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? BenefitCreatedDate { get; set; }

        public string? BenefitCreatedBy { get; set; } = null!;

        public DateTime? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; } = null!;
    }
}
