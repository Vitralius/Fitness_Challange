using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using fitnessapp.Models;

namespace fitnessapp.Data;

public partial class ChallengeDatabaseContext : DbContext
{
    public ChallengeDatabaseContext()
    {
    }

    public ChallengeDatabaseContext(DbContextOptions<ChallengeDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblChallenge> TblChallenges { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = WebApplication.CreateBuilder();
        var connectionString = builder.Configuration.GetConnectionString ("SecondConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblChallenge>(entity =>
        {
            entity.HasKey(e => e.ChallengeId);

            entity.ToTable("tbl_challenge");

            entity.Property(e => e.ChallengeId).HasColumnName("challengeId");
            entity.Property(e => e.Category)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("category");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("endDate");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.ParentId).HasColumnName("parentId");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
