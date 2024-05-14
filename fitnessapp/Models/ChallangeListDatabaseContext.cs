using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace fitnessapp.Models;

public partial class ChallangeListDatabaseContext : DbContext
{
    public ChallangeListDatabaseContext()
    {
    }

    public ChallangeListDatabaseContext(DbContextOptions<ChallangeListDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblChallangelist> TblChallangelists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress; Database=ChallangeListDatabase; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblChallangelist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbl_chal__3213E83F5D24B0F9");

            entity.ToTable("tbl_challangelist");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("endDate");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
