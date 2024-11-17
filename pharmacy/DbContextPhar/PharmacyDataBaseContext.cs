using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using pharmacy.models;

namespace pharmacy.DbContextPhar;

public partial class PharmacyDataBaseContext : DbContext
{
    public PharmacyDataBaseContext()
    {
    }

    public PharmacyDataBaseContext(DbContextOptions<PharmacyDataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DrugsOnParHand> DrugsOnParHands { get; set; }

    public virtual DbSet<DrugsOnSupHand> DrugsOnSupHands { get; set; }

    public virtual DbSet<Pharmacy> Pharmacies { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=C:\\Users\\bubblechuk\\Desktop\\pharmacy\\database\\pharmacyDataBase.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DrugsOnParHand>(entity =>
        {
            entity.HasKey(e => e.DrugId);

            entity.ToTable("drugsOnParHands");

            entity.Property(e => e.DrugId).HasColumnName("drug_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.BestBeforeDate).HasColumnName("\nbest_before_date");
            entity.Property(e => e.DrugName).HasColumnName("drug_name");
            entity.Property(e => e.Filling).HasColumnName("filling");
            entity.Property(e => e.PharmacyId).HasColumnName("pharmacy_id");
            entity.Property(e => e.Price)
                .HasColumnType("NUMERIC")
                .HasColumnName("price");

            entity.HasOne(d => d.Pharmacy).WithMany(p => p.DrugsOnParHands).HasForeignKey(d => d.PharmacyId);
        });

        modelBuilder.Entity<DrugsOnSupHand>(entity =>
        {
            entity.HasKey(e => e.DrugId);

            entity.ToTable("drugsOnSupHands");

            entity.Property(e => e.DrugId).HasColumnName("drug_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.DrugName).HasColumnName("drug_name");
            entity.Property(e => e.Prior).HasColumnName("prior");
            entity.Property(e => e.Filling).HasColumnName("filling");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

            entity.HasOne(d => d.Supplier).WithMany(p => p.DrugsOnSupHands).HasForeignKey(d => d.SupplierId);
        });

        modelBuilder.Entity<Pharmacy>(entity =>
        {
            entity.ToTable("pharmacy");

            entity.Property(e => e.PharmacyId).HasColumnName("pharmacy_id");
            entity.Property(e => e.Adress).HasColumnName("adress");
            entity.Property(e => e.SupplierId).HasColumnName("Supplier_id");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Pharmacies).HasForeignKey(d => d.SupplierId);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("supplier");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
