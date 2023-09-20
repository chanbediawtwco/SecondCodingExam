using System;
using System.Collections.Generic;

namespace SecondCodingExam.Models;

public partial class Calculation
{
    public int Id { get; set; }

    public int BenefitId { get; set; }

    public int CustomerId { get; set; }

    public int BenefitsAmountQuotation { get; set; }

    public int PendedAmount { get; set; }

    public int CurrentBenefit { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsRecalculated { get; set; }

    public virtual Benefit Benefit { get; set; } = null!;

    public virtual Cutomer Customer { get; set; } = null!;
}
