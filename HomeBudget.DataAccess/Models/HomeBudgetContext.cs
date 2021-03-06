﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HomeBudget.DataAccess.Models
{
    public partial class HomeBudgetContext : DbContext
    {
        public HomeBudgetContext()
        {
        }

        public HomeBudgetContext(DbContextOptions<HomeBudgetContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Expenses> Expenses { get; set; }
        public virtual DbSet<Units> Units { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                AppConfiguration appConfiguration = new AppConfiguration();
                optionsBuilder.UseSqlServer(appConfiguration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Categories_Users");
            });

            modelBuilder.Entity<Expenses>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("smalldatetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Expenses_Categories");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_Expenses_Units");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Expenses_Users");
            });

            modelBuilder.Entity<Units>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
