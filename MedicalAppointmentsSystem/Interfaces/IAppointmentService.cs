using patient_management_system.Models;

namespace patient_management_system.Interfaces;

public interface IAppointmentService
{
    void Schedule(Guid patientId, Guid doctorId, DateTime scheduledAt);
    void Cancel(Guid appointmentId);
    void Complete(Guid appointmentId);
    List<Appointment> GetByPatient(Guid patientId);
    List<Appointment> GetByDoctor(Guid doctorId);
    bool ValidateConflict(Guid doctorId, DateTime scheduledAt);
    Appointment? GetById(Guid id);
}
