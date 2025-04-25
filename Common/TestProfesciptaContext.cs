using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVCNetCore;

public partial class TestProfesciptaContext : DbContext
{
    public TestProfesciptaContext()
    {
    }

    public TestProfesciptaContext(DbContextOptions<TestProfesciptaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ComCustomer> ComCustomers { get; set; }

    public virtual DbSet<SoItem> SoItems { get; set; }

    public virtual DbSet<SoOrder> SoOrders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-GQBP2AS\\SQLEXPRESS;Database=TestProfescipta;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComCustomer>(entity =>
        {
            entity.ToTable("COM_CUSTOMER");

            entity.Property(e => e.ComCustomerId).HasColumnName("COM_CUSTOMER_ID");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("CUSTOMER_NAME");
        });

        modelBuilder.Entity<SoItem>(entity =>
        {
            entity.ToTable("SO_ITEM");

            entity.Property(e => e.SoItemId).HasColumnName("SO_ITEM_ID");
            entity.Property(e => e.ItemName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("")
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("ITEM_NAME");
            entity.Property(e => e.Price).HasColumnName("PRICE");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(-99)
                .HasColumnName("QUANTITY");
            entity.Property(e => e.SoOrderId)
                .HasDefaultValue(-99L)
                .HasColumnName("SO_ORDER_ID");
        });

        modelBuilder.Entity<SoOrder>(entity =>
        {
            entity.ToTable("SO_ORDER");

            entity.Property(e => e.SoOrderId).HasColumnName("SO_ORDER_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("")
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("ADDRESS");
            entity.Property(e => e.ComCustomerId)
                .HasDefaultValueSql("('-99')")
                .HasColumnName("COM_CUSTOMER_ID");
            entity.Property(e => e.OrderDate)
                .HasDefaultValue(new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("ORDER_DATE");
            entity.Property(e => e.OrderNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("")
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("ORDER_NO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
