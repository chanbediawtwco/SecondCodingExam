using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SecondCodingExam.Models;

namespace SecondCodingExam.Data;

public partial class SecondCodingExamDbContext : DbContext
{
    public SecondCodingExamDbContext()
    {
    }

    public SecondCodingExamDbContext(DbContextOptions<SecondCodingExamDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Benefit> Benefits { get; set; }

    public virtual DbSet<BenefitsHistory> BenefitsHistories { get; set; }

    public virtual DbSet<Calculation> Calculations { get; set; }

    public virtual DbSet<CalculationsHistory> CalculationsHistories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomersBenefitsHistory> CustomersBenefitsHistories { get; set; }

    public virtual DbSet<CustomersCurrentBenefit> CustomersCurrentBenefits { get; set; }

    public virtual DbSet<CustomersHistory> CustomersHistories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Benefit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Benefits__3214EC07416C8CB9");

            entity.Property(e => e.CreatedBy).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Benefits)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Benefits__UserId__69B1A35C");
        });

        modelBuilder.Entity<BenefitsHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Benefits__3214EC07E1A1F485");

            entity.Property(e => e.BenefitCreatedBy).HasMaxLength(255);
            entity.Property(e => e.BenefitCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Benefit).WithMany(p => p.BenefitsHistories)
                .HasForeignKey(d => d.BenefitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BenefitsH__Benef__6F6A7CB2");

            entity.HasOne(d => d.User).WithMany(p => p.BenefitsHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BenefitsH__UserI__6E765879");
        });

        modelBuilder.Entity<Calculation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Calculat__3214EC07F9DBD225");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CurrentBenefit).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Calculations)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calculati__Custo__0A1E72EE");

            entity.HasOne(d => d.CustomersCurrentBenefits).WithMany(p => p.Calculations)
                .HasForeignKey(d => d.CustomersCurrentBenefitsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calculati__Custo__092A4EB5");
        });

        modelBuilder.Entity<CalculationsHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Calculat__3214EC07779D7A0D");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CurrentBenefit).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.CalculationsHistories)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calculati__Custo__0EE3280B");

            entity.HasOne(d => d.CustomersCurrentBenefits).WithMany(p => p.CalculationsHistories)
                .HasForeignKey(d => d.CustomersCurrentBenefitsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calculati__Custo__0DEF03D2");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC073B4BCC39");

            entity.Property(e => e.Birthdate).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Firstname).HasMaxLength(255);
            entity.Property(e => e.Lastname).HasMaxLength(255);
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Customers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__UserI__733B0D96");
        });

        modelBuilder.Entity<CustomersBenefitsHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC076F755BEE");

            entity.Property(e => e.BenefitCreatedBy).HasMaxLength(255);
            entity.Property(e => e.BenefitCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomersBenefitsHistories)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Custo__009508B4");

            entity.HasOne(d => d.CustomersCurrentBenefits).WithMany(p => p.CustomersBenefitsHistories)
                .HasForeignKey(d => d.CustomersCurrentBenefitsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Custo__7FA0E47B");

            entity.HasOne(d => d.User).WithMany(p => p.CustomersBenefitsHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__UserI__7EACC042");
        });

        modelBuilder.Entity<CustomersCurrentBenefit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07CFCC45FD");

            entity.Property(e => e.CreatedBy).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Benefit).WithMany(p => p.CustomersCurrentBenefits)
                .HasForeignKey(d => d.BenefitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Benef__78F3E6EC");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomersCurrentBenefits)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Custo__79E80B25");

            entity.HasOne(d => d.User).WithMany(p => p.CustomersCurrentBenefits)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__UserI__77FFC2B3");
        });

        modelBuilder.Entity<CustomersHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07E5ED3DFD");

            entity.Property(e => e.Birthdate).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Firstname).HasMaxLength(255);
            entity.Property(e => e.Lastname).HasMaxLength(255);
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomersHistories)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Custo__04659998");

            entity.HasOne(d => d.CustomersBenefitsHistory).WithMany(p => p.CustomersHistories)
                .HasForeignKey(d => d.CustomersBenefitsHistoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Custo__0559BDD1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0792C26DDE");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Firstname).HasMaxLength(255);
            entity.Property(e => e.Lastname).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
