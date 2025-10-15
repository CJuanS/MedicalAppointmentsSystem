namespace patient_management_system.Models;

public class EmailLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AppointmentId { get; set; }
    public string Email { get; set; } = string.Empty;
    public EmailStatus Status { get; set; }
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    public void RegisterEmail(Guid appointmentId, string email, EmailStatus status)
    {
        AppointmentId = appointmentId;
        Email = email;
        Status = status;
        TimeStamp = DateTime.Now;
    }

    public void UpdateStatus(EmailStatus newStatus)
    {
        Status = newStatus;
    }
}
