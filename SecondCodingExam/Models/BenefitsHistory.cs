﻿using SecondCodingExam.Services;
using System;
using System.Collections.Generic;

namespace SecondCodingExam.Models;

public partial class BenefitsHistory: GenericAuditClassForHistory
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int BenefitId { get; set; }

    public int GuaranteedIssue { get; set; }

    public int MaxAgeLimit { get; set; }

    public int MinAgeLimit { get; set; }

    public int MaxRange { get; set; }

    public int MinRange { get; set; }

    public int Increments { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Benefit Benefit { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
