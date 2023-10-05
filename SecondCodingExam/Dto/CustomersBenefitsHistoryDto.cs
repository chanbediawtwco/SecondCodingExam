namespace SecondCodingExam.Dto
{
    public class CustomersBenefitsHistoryDto
    {
        public int Id { get; set; }

        public int CustomersCurrentBenefitsId { get; set; }

        public int GuaranteedIssue { get; set; }

        public int MaxAgeLimit { get; set; }

        public int MinAgeLimit { get; set; }

        public int MaxRange { get; set; }

        public int MinRange { get; set; }

        public int Increments { get; set; }
    }
}
