using System;
using System.Collections.Generic;

namespace SecondCodingExam.Models;

public partial class Calculation
{
    public int Id { get; set; }

    public int CustomersCurrentBenefitsId { get; set; }

    public int CustomerId { get; set; }

    public int Multiple { get; set; }

    public int BenefitsAmountQuotation { get; set; }

    public int PendedAmount { get; set; }

    public int CurrentBenefit { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsBenefitPending { get; set; }

    public bool IsRecalculated { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual CustomersCurrentBenefit CustomersCurrentBenefits { get; set; } = null!;
}
