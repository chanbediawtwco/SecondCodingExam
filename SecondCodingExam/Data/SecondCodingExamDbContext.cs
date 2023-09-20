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

    public virtual DbSet<Cutomer> Cutomers { get; set; }

    public virtual DbSet<CutomersHistory> CutomersHistories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Benefit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Benefits__3214EC078D0FC99B");

            entity.Property(e => e.CreatedBy).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Benefits)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Benefits__UserId__5441852A");
        });

        modelBuilder.Entity<BenefitsHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Benefits__3214EC07D5EBC62E");

            entity.Property(e => e.BenefitCreatedBy).HasMaxLength(255);
            entity.Property(e => e.BenefitCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Benefit).WithMany(p => p.BenefitsHistories)
                .HasForeignKey(d => d.BenefitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BenefitsH__Benef__59FA5E80");

            entity.HasOne(d => d.User).WithMany(p => p.BenefitsHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BenefitsH__UserI__59063A47");
        });

        modelBuilder.Entity<Calculation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Calculat__3214EC075FAC8832");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Benefit).WithMany(p => p.Calculations)
                .HasForeignKey(d => d.BenefitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calculati__Benef__6754599E");

            entity.HasOne(d => d.Customer).WithMany(p => p.Calculations)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calculati__Custo__68487DD7");
        });

        modelBuilder.Entity<CalculationsHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Calculat__3214EC074C1D9EE3");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.BenefitsHistory).WithMany(p => p.CalculationsHistories)
                .HasForeignKey(d => d.BenefitsHistoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calculati__Benef__6C190EBB");

            entity.HasOne(d => d.Customer).WithMany(p => p.CalculationsHistories)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calculati__Custo__6D0D32F4");
        });

        modelBuilder.Entity<Cutomer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cutomers__3214EC07C2A0C3B1");

            entity.Property(e => e.Birthdate).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Firstname).HasMaxLength(255);
            entity.Property(e => e.Lastname).HasMaxLength(255);
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Benefit).WithMany(p => p.Cutomers)
                .HasForeignKey(d => d.BenefitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cutomers__Benefi__5EBF139D");

            entity.HasOne(d => d.User).WithMany(p => p.Cutomers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cutomers__UserId__5DCAEF64");
        });

        modelBuilder.Entity<CutomersHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cutomers__3214EC07EF8AFACE");

            entity.Property(e => e.Birthdate).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Firstname).HasMaxLength(255);
            entity.Property(e => e.Lastname).HasMaxLength(255);
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.BenefitsHistory).WithMany(p => p.CutomersHistories)
                .HasForeignKey(d => d.BenefitsHistoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CutomersH__Benef__6477ECF3");

            entity.HasOne(d => d.Customer).WithMany(p => p.CutomersHistories)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CutomersH__Custo__6383C8BA");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC074FA60E79");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Firstname).HasMaxLength(255);
            entity.Property(e => e.Lastname).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
