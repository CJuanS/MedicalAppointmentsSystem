namespace patient_management_system.Models;

public class Appointment
{
    public Guid Id { get; set; } = Guid.NewGuid();

    // relacion por Id (FK)
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }

    // Fecha y hora de la cita -> nombre claro
    public DateTime ScheduledAt { get; set; }

    // Estado
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
