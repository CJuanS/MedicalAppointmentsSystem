using Microsoft.EntityFrameworkCore;
using patient_management_system.Models;

namespace patient_management_system.Data;

public class AppDbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<Appointment> Appointments { get; set; } = null!;
    public DbSet<EmailLog> EmailLogs { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>()
            .HasIndex(p => p.DocumentNumber)
            .IsUnique();

        modelBuilder.Entity<Doctor>()
            .HasIndex(d => d.DocumentNumber)
            .IsUnique();


        modelBuilder.Entity<Appointment>()
            .HasIndex(a => new { a.DoctorId, a.ScheduledAt })
            .IsUnique(false);

        modelBuilder.Entity<Appointment>()
            .Property(a => a.ScheduledAt)
            .HasColumnType("timestamp without time zone");

        base.OnModelCreating(modelBuilder);
    }
}
