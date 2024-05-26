using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using fitnessapp.Models;

namespace fitnessapp.Data;

public partial class UserChallengeDatabaseContext : DbContext
{
    public UserChallengeDatabaseContext()
    {
    }

    public UserChallengeDatabaseContext(DbContextOptions<UserChallengeDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Challenge> Challenges { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Participate> Participates { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    public virtual DbSet<UserRate> UserRates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = WebApplication.CreateBuilder();
        var connectionString = builder.Configuration.GetConnectionString ("MyConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Challenge>(entity =>
        {
            entity.ToTable("challenge");

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
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("title");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("userId");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("city");

            entity.Property(e => e.CityId).HasColumnName("cityId");
            entity.Property(e => e.CityName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cityName");
        });

        modelBuilder.Entity<Participate>(entity =>
        {
            entity.ToTable("participate");

            entity.Property(e => e.ParticipateId).HasColumnName("participateId");
            entity.Property(e => e.ChallengeId).HasColumnName("challengeId");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("userId");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.DetailId);

            entity.ToTable("userDetail");

            entity.Property(e => e.DetailId).HasColumnName("detailId");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("userId");
        });

        modelBuilder.Entity<UserRate>(entity =>
        {
            entity.HasKey(e => e.RateId);

            entity.ToTable("userRate");

            entity.Property(e => e.RateId).HasColumnName("rateId");
            entity.Property(e => e.ChallengeId).HasColumnName("challengeId");
            entity.Property(e => e.Rate).HasColumnName("rate");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("userId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
