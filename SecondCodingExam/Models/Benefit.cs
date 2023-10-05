﻿using SecondCodingExam.Services;
using System;
using System.Collections.Generic;

namespace SecondCodingExam.Models;

public partial class Benefit: GenericAuditClass
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int GuaranteedIssue { get; set; }

    public int MaxAgeLimit { get; set; }

    public int MinAgeLimit { get; set; }

    public int MaxRange { get; set; }

    public int MinRange { get; set; }

    public int Increments { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsUpdated { get; set; }

    public virtual ICollection<BenefitsHistory> BenefitsHistories { get; set; } = new List<BenefitsHistory>();

    public virtual ICollection<CustomersCurrentBenefit> CustomersCurrentBenefits { get; set; } = new List<CustomersCurrentBenefit>();

    public virtual User User { get; set; } = null!;
}
