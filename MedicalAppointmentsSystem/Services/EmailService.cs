using patient_management_system.Models;
using patient_management_system.Interfaces;

namespace patient_management_system.Services;

public class EmailService : IEmailService
{
    private readonly List<EmailLog> _emailLogs = new();

    public void SendConfirmationEmail(string to, string subject, string body)
    {
        Console.WriteLine($"Sending email to {to}...");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Body: {body}");
        Console.WriteLine("Email sent successfully (simulated).");
    }

    public void LogEmail(Guid appointmentId, string email, EmailStatus status)
    {
        var log = new EmailLog();
        log.RegisterEmail(appointmentId, email, status);
        _emailLogs.Add(log);
    }

    public List<EmailLog> GetAllLogs() => _emailLogs;
}
