using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Mimun.HomeAssignment.Models;

public partial class MimunDbContext : DbContext
{
    public MimunDbContext()
    {
    }

    public MimunDbContext(DbContextOptions<MimunDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<ContractType> ContractTypes { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<PackageType> PackageTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=nas,1433;Database=MimunDB;TrustServerCertificate=True;User ID=sa;Password=jacui4Nhftk;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contract>(entity =>
        {
            entity.ToTable("Contract");

            entity.HasIndex(e => e.ContractNumber, "IX_Contract").IsUnique();

            entity.Property(e => e.ContractNumber).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contract_Customer");

            entity.HasOne(d => d.Type).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Contract_ContractType");
        });

        modelBuilder.Entity<ContractType>(entity =>
        {
            entity.ToTable("ContractType");

            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.HasIndex(e => e.IdNumber, "IX_Customer").IsUnique();

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.HouseNumber).HasMaxLength(50);
            entity.Property(e => e.IdNumber)
                .HasMaxLength(9)
                .IsUnicode(false);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PostalCode).HasMaxLength(50);
            entity.Property(e => e.Street).HasMaxLength(50);
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.ToTable("Package");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.PackageName).HasMaxLength(50);

            entity.HasOne(d => d.Contract).WithMany(p => p.Packages)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Package_Contract");

            entity.HasOne(d => d.PackageType).WithMany(p => p.Packages)
                .HasForeignKey(d => d.PackageTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Package_PackageType");
        });

        modelBuilder.Entity<PackageType>(entity =>
        {
            entity.ToTable("PackageType");

            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
