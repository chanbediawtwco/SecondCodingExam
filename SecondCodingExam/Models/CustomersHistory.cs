using SecondCodingExam.Services;
using System;
using System.Collections.Generic;

namespace SecondCodingExam.Models;

public partial class CustomersHistory: GenericAuditClass
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int CustomersBenefitsHistoryId { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public int BasicSalary { get; set; }

    public DateTime Birthdate { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual CustomersBenefitsHistory CustomersBenefitsHistory { get; set; } = null!;
}
