using SecondCodingExam.Services;
using System;
using System.Collections.Generic;

namespace SecondCodingExam.Models;

public partial class Customer: GenericAuditClass
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public int BasicSalary { get; set; }

    public DateTime Birthdate { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsUpdated { get; set; }

    public virtual ICollection<Calculation> Calculations { get; set; } = new List<Calculation>();

    public virtual ICollection<CalculationsHistory> CalculationsHistories { get; set; } = new List<CalculationsHistory>();

    public virtual ICollection<CustomersBenefitsHistory> CustomersBenefitsHistories { get; set; } = new List<CustomersBenefitsHistory>();

    public virtual ICollection<CustomersCurrentBenefit> CustomersCurrentBenefits { get; set; } = new List<CustomersCurrentBenefit>();

    public virtual ICollection<CustomersHistory> CustomersHistories { get; set; } = new List<CustomersHistory>();

    public virtual User User { get; set; } = null!;
}
