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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=.;initial catalog=SecondCodingExam;trusted_connection=true;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Benefit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Benefits__3214EC0799DB1948");

            entity.Property(e => e.CreatedBy).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Benefits)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Benefits__UserId__13A7DD28");
        });

        modelBuilder.Entity<BenefitsHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Benefits__3214EC07CEE5236E");

            entity.Property(e => e.BenefitCreatedBy).HasMaxLength(255);
            entity.Property(e => e.BenefitCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Benefit).WithMany(p => p.BenefitsHistories)
                .HasForeignKey(d => d.BenefitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BenefitsH__Benef__1960B67E");

            entity.HasOne(d => d.User).WithMany(p => p.BenefitsHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BenefitsH__UserI__186C9245");
        });

        modelBuilder.Entity<Calculation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Calculat__3214EC078A438256");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Calculations)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calculati__Custo__3414ACBA");

            entity.HasOne(d => d.CustomersCurrentBenefits).WithMany(p => p.Calculations)
                .HasForeignKey(d => d.CustomersCurrentBenefitsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calculati__Custo__33208881");
        });

        modelBuilder.Entity<CalculationsHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Calculat__3214EC0787208839");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.CalculationsHistories)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calculati__Custo__39CD8610");

            entity.HasOne(d => d.CustomersCurrentBenefits).WithMany(p => p.CalculationsHistories)
                .HasForeignKey(d => d.CustomersCurrentBenefitsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calculati__Custo__38D961D7");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC0789315174");

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
                .HasConstraintName("FK__Customers__UserI__1D314762");
        });

        modelBuilder.Entity<CustomersBenefitsHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07EE5D1E10");

            entity.Property(e => e.BenefitCreatedBy).HasMaxLength(255);
            entity.Property(e => e.BenefitCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomersBenefitsHistories)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Custo__2A8B4280");

            entity.HasOne(d => d.CustomersCurrentBenefits).WithMany(p => p.CustomersBenefitsHistories)
                .HasForeignKey(d => d.CustomersCurrentBenefitsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Custo__29971E47");

            entity.HasOne(d => d.User).WithMany(p => p.CustomersBenefitsHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__UserI__28A2FA0E");
        });

        modelBuilder.Entity<CustomersCurrentBenefit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC073E4C2D59");

            entity.Property(e => e.CreatedBy).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Benefit).WithMany(p => p.CustomersCurrentBenefits)
                .HasForeignKey(d => d.BenefitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Benef__22EA20B8");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomersCurrentBenefits)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Custo__23DE44F1");

            entity.HasOne(d => d.User).WithMany(p => p.CustomersCurrentBenefits)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__UserI__21F5FC7F");
        });

        modelBuilder.Entity<CustomersHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07C2F7CAE6");

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
                .HasConstraintName("FK__Customers__Custo__2E5BD364");

            entity.HasOne(d => d.CustomersBenefitsHistory).WithMany(p => p.CustomersHistories)
                .HasForeignKey(d => d.CustomersBenefitsHistoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Custo__2F4FF79D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC075083930D");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Firstname).HasMaxLength(255);
            entity.Property(e => e.Lastname).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
