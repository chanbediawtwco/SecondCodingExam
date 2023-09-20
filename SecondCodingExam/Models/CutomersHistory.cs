using System;
using System.Collections.Generic;

namespace SecondCodingExam.Models;

public partial class CutomersHistory
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int BenefitsHistoryId { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public int BasicSalary { get; set; }

    public DateTime Birthdate { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public virtual BenefitsHistory BenefitsHistory { get; set; } = null!;

    public virtual Cutomer Customer { get; set; } = null!;
}
