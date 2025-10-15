using patient_management_system.Models;

namespace patient_management_system.Interfaces;

public interface IEmailService
{
    void SendConfirmationEmail(string to, string subject, string body);
    void LogEmail(Guid appointmentId, string email, EmailStatus status);
    List<EmailLog> GetAllLogs();
}
