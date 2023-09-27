using System;
using System.Collections.Generic;

namespace SecondCodingExam.Models;

public partial class User
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Benefit> Benefits { get; set; } = new List<Benefit>();

    public virtual ICollection<BenefitsHistory> BenefitsHistories { get; set; } = new List<BenefitsHistory>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<CustomersBenefitsHistory> CustomersBenefitsHistories { get; set; } = new List<CustomersBenefitsHistory>();

    public virtual ICollection<CustomersCurrentBenefit> CustomersCurrentBenefits { get; set; } = new List<CustomersCurrentBenefit>();
}
