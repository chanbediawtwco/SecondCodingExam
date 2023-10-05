namespace SecondCodingExam.Dto
{
    public class CalculationDto
    {
        public int Id { get; set; }

        public int CustomersCurrentBenefitsId { get; set; }

        public int CustomerId { get; set; }

        public int Multiple { get; set; }

        public int BenefitsAmountQuotation { get; set; }

        public int PendedAmount { get; set; }

        public int CurrentBenefit { get; set; }

        public bool IsBenefitPending { get; set; }
    }
}
