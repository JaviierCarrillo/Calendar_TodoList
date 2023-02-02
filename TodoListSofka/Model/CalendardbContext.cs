﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TodoListSofka.Model;

public partial class CalendardbContext : DbContext
{
    public CalendardbContext()
    {
    }

    public CalendardbContext(DbContextOptions<CalendardbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calendar> Calendars { get; set; }

    public virtual DbSet<Day> Days { get; set; }

    public virtual DbSet<Todoitem> Todoitems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=CALENDARDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calendar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CALENDAR__3214EC0701D9B683");

            entity.ToTable("CALENDARS");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Day>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DAYS__3214EC07612192D3");

            entity.ToTable("DAYS");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.IdCalendarNavigation).WithMany(p => p.Days)
                .HasForeignKey(d => d.IdCalendar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DAYS__IdCalendar__3E52440B");
        });

        modelBuilder.Entity<Todoitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TODOITEM__3214EC07F60E8171");

            entity.ToTable("TODOITEMS");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Responsible)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDayNavigation).WithMany(p => p.Todoitems)
                .HasForeignKey(d => d.IdDay)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TODOITEMS__IdDay__4222D4EF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
