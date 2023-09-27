using System;
using System.Collections.Generic;

namespace SecondCodingExam.Models;

public partial class CustomersCurrentBenefit
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int BenefitId { get; set; }

    public int CustomerId { get; set; }

    public int GuaranteedIssue { get; set; }

    public int MaxAgeLimit { get; set; }

    public int MinAgeLimit { get; set; }

    public int MaxRange { get; set; }

    public int MinRange { get; set; }

    public int Increments { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsUpdated { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual Benefit Benefit { get; set; } = null!;

    public virtual ICollection<Calculation> Calculations { get; set; } = new List<Calculation>();

    public virtual ICollection<CalculationsHistory> CalculationsHistories { get; set; } = new List<CalculationsHistory>();

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<CustomersBenefitsHistory> CustomersBenefitsHistories { get; set; } = new List<CustomersBenefitsHistory>();

    public virtual User User { get; set; } = null!;
}
