using System;
using System.Collections.Generic;

namespace SecondCodingExam.Models;

public partial class CustomersBenefitsHistory
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CustomersCurrentBenefitsId { get; set; }

    public int GuaranteedIssue { get; set; }

    public int MaxAgeLimit { get; set; }

    public int MinAgeLimit { get; set; }

    public int MaxRange { get; set; }

    public int MinRange { get; set; }

    public int Increments { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime BenefitCreatedDate { get; set; }

    public string BenefitCreatedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public virtual CustomersCurrentBenefit CustomersCurrentBenefits { get; set; } = null!;

    public virtual ICollection<CustomersHistory> CustomersHistories { get; set; } = new List<CustomersHistory>();

    public virtual User User { get; set; } = null!;
}
