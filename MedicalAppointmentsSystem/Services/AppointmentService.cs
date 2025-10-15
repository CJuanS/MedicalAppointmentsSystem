using patient_management_system.Data;
using patient_management_system.Models;
using Microsoft.EntityFrameworkCore;

namespace patient_management_system.Interfaces;

public class AppointmentService : IAppointmentService
{
    private readonly AppDbContext _context;

    public AppointmentService(AppDbContext context)
    {
        _context = context;
    }

    public void Schedule(Guid patientId, Guid doctorId, DateTime scheduledAt)
    {
        // basic validation
        var patient = _context.Patients.Find(patientId);
        if (patient == null) throw new Exception("Patient not found.");

        var doctor = _context.Doctors.Find(doctorId);
        if (doctor == null) throw new Exception("Doctor not found.");

        // Conflict checks: same doctor or same patient at same exact datetime
        if (_context.Appointments.Any(a => a.DoctorId == doctorId && a.ScheduledAt == scheduledAt && a.Status == AppointmentStatus.Scheduled))
            throw new Exception("Doctor already has an appointment at this time.");

        if (_context.Appointments.Any(a => a.PatientId == patientId && a.ScheduledAt == scheduledAt && a.Status == AppointmentStatus.Scheduled))
            throw new Exception("Patient already has an appointment at this time.");


        scheduledAt = DateTime.SpecifyKind(scheduledAt, DateTimeKind.Unspecified);

        var appointment = new Appointment
        {
            PatientId = patientId,
            DoctorId = doctorId,
            ScheduledAt = scheduledAt,
            Status = AppointmentStatus.Scheduled
        };



        _context.Appointments.Add(appointment);
        _context.SaveChanges();
    }

    public void Cancel(Guid appointmentId)
    {
        var appointment = _context.Appointments.Find(appointmentId);
        if (appointment == null) throw new Exception("Appointment not found.");

        appointment.Status = AppointmentStatus.Cancelled;
        _context.SaveChanges();
    }

    public void Complete(Guid appointmentId)
    {
        var appointment = _context.Appointments.Find(appointmentId);
        if (appointment == null) throw new Exception("Appointment not found.");

        appointment.Status = AppointmentStatus.Completed;
        _context.SaveChanges();
    }

    public List<Appointment> GetByPatient(Guid patientId)
        => _context.Appointments.AsNoTracking().Where(a => a.PatientId == patientId).ToList();

    public List<Appointment> GetByDoctor(Guid doctorId)
        => _context.Appointments.AsNoTracking().Where(a => a.DoctorId == doctorId).ToList();

    public bool ValidateConflict(Guid doctorId, DateTime scheduledAt)
        => _context.Appointments.Any(a => a.DoctorId == doctorId && a.ScheduledAt == scheduledAt && a.Status == AppointmentStatus.Scheduled);

    public Appointment? GetById(Guid id)
        => _context.Appointments.Find(id);
}
