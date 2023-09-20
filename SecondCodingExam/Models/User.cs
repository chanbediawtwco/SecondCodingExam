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

    public virtual ICollection<Cutomer> Cutomers { get; set; } = new List<Cutomer>();
}
