using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PC1.DOMAIN.CORE.Entities;

namespace PC1.DOMAIN.INFRAESTRUCTURES.Data;

public partial class EventManagementDbContext : DbContext
{
    public EventManagementDbContext()
    {
    }

    public EventManagementDbContext(DbContextOptions<EventManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendees> Attendees { get; set; }

    public virtual DbSet<EventAttendance> EventAttendance { get; set; }

    public virtual DbSet<Events> Events { get; set; }

    public virtual DbSet<Organizers> Organizers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=EventManagementDB;User=sa;Pwd=123456789;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendees>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Attendee__3214EC07AEB9E525");

            entity.Property(e => e.AttendeeName).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.RegisteredAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<EventAttendance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EventAtt__3214EC0797F29021");

            entity.Property(e => e.CheckedInAt).HasColumnType("datetime");

            entity.HasOne(d => d.Attendee).WithMany(p => p.EventAttendance)
                .HasForeignKey(d => d.AttendeeId)
                .HasConstraintName("FK__EventAtte__Atten__403A8C7D");

            entity.HasOne(d => d.Event).WithMany(p => p.EventAttendance)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__EventAtte__Event__412EB0B6");
        });

        modelBuilder.Entity<Events>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Events__3214EC07DD4E70E1");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EventDate).HasColumnType("datetime");
            entity.Property(e => e.EventName).HasMaxLength(100);
            entity.Property(e => e.Location).HasMaxLength(200);

            entity.HasOne(d => d.Organizer).WithMany(p => p.Events)
                .HasForeignKey(d => d.OrganizerId)
                .HasConstraintName("FK__Events__Organize__4222D4EF");
        });

        modelBuilder.Entity<Organizers>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Organize__3214EC071D5CEC40");

            entity.Property(e => e.ContactEmail).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrganizerName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
