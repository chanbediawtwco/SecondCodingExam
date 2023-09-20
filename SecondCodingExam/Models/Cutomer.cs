﻿using System;
using System.Collections.Generic;

namespace SecondCodingExam.Models;

public partial class Cutomer
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int BenefitId { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public int BasicSalary { get; set; }

    public DateTime Birthdate { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsUpdated { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual Benefit Benefit { get; set; } = null!;

    public virtual ICollection<Calculation> Calculations { get; set; } = new List<Calculation>();

    public virtual ICollection<CalculationsHistory> CalculationsHistories { get; set; } = new List<CalculationsHistory>();

    public virtual ICollection<CutomersHistory> CutomersHistories { get; set; } = new List<CutomersHistory>();

    public virtual User User { get; set; } = null!;
}
