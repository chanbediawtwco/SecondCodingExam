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
            entity.HasKey(e => e.Id).HasName("PK__Benefits__3214EC0701C43EDD");

            entity.Property(e => e.CreatedBy).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Benefits)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Benefits__UserId__02284B6B");
        });

        modelBuilder.Entity<BenefitsHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Benefits__3214EC074CE64EE3");

            entity.Property(e => e.BenefitCreatedBy).HasMaxLength(255);
            entity.Property(e => e.BenefitCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Benefit).WithMany(p => p.BenefitsHistories)
                .HasForeignKey(d => d.BenefitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BenefitsH__Benef__07E124C1");

            entity.HasOne(d => d.User).WithMany(p => p.BenefitsHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BenefitsH__UserI__06ED0088");
        });

        modelBuilder.Entity<Calculation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Calculat__3214EC07337996F1");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CurrentBenefit).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Calculations)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calculati__Custo__20ACD28B");

            entity.HasOne(d => d.CustomersCurrentBenefits).WithMany(p => p.Calculations)
                .HasForeignKey(d => d.CustomersCurrentBenefitsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calculati__Custo__1FB8AE52");
        });

        modelBuilder.Entity<CalculationsHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Calculat__3214EC0714F89CA8");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CurrentBenefit).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.CalculationsHistories)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calculati__Custo__257187A8");

            entity.HasOne(d => d.CustomersCurrentBenefits).WithMany(p => p.CalculationsHistories)
                .HasForeignKey(d => d.CustomersCurrentBenefitsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calculati__Custo__247D636F");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07D9FDB3B1");

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
                .HasConstraintName("FK__Customers__UserI__0BB1B5A5");
        });

        modelBuilder.Entity<CustomersBenefitsHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07974E2070");

            entity.Property(e => e.BenefitCreatedBy).HasMaxLength(255);
            entity.Property(e => e.BenefitCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.CustomersCurrentBenefits).WithMany(p => p.CustomersBenefitsHistories)
                .HasForeignKey(d => d.CustomersCurrentBenefitsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Custo__18178C8A");

            entity.HasOne(d => d.User).WithMany(p => p.CustomersBenefitsHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__UserI__17236851");
        });

        modelBuilder.Entity<CustomersCurrentBenefit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07FB8FE257");

            entity.Property(e => e.CreatedBy).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Benefit).WithMany(p => p.CustomersCurrentBenefits)
                .HasForeignKey(d => d.BenefitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Benef__116A8EFB");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomersCurrentBenefits)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Custo__125EB334");

            entity.HasOne(d => d.User).WithMany(p => p.CustomersCurrentBenefits)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__UserI__10766AC2");
        });

        modelBuilder.Entity<CustomersHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC0722E8C235");

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
                .HasConstraintName("FK__Customers__Custo__1BE81D6E");

            entity.HasOne(d => d.CustomersBenefitsHistory).WithMany(p => p.CustomersHistories)
                .HasForeignKey(d => d.CustomersBenefitsHistoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Custo__1CDC41A7");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC078D8FCBFE");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Firstname).HasMaxLength(255);
            entity.Property(e => e.Lastname).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
