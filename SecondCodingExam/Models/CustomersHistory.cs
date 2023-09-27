using System;
using System.Collections.Generic;

namespace SecondCodingExam.Models;

public partial class CustomersHistory
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int CustomersBenefitsHistoryId { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public int BasicSalary { get; set; }

    public DateTime Birthdate { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual CustomersBenefitsHistory CustomersBenefitsHistory { get; set; } = null!;
}
