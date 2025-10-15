using patient_management_system.Interfaces;
using patient_management_system.Models;
using Microsoft.Extensions.DependencyInjection;
using patient_management_system.Utils;

namespace patient_management_system.Menus;

public class AppointmentMenu
{
    private readonly IServiceProvider _provider;

    public AppointmentMenu(IServiceProvider provider)
    {
        _provider = provider;
    }

    public void Show()
    {
        var svc = _provider.CreateScope().ServiceProvider.GetRequiredService<IAppointmentService>();
        var patientSvc = _provider.CreateScope().ServiceProvider.GetRequiredService<IPatientService>();
        var doctorSvc = _provider.CreateScope().ServiceProvider.GetRequiredService<IDoctorService>();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Appointments Menu ===");
            Console.WriteLine("1. Schedule appointment");
            Console.WriteLine("2. List appointments by patient");
            Console.WriteLine("3. List appointments by doctor");
            Console.WriteLine("4. Cancel appointment");
            Console.WriteLine("5. Mark appointment as attended");
            Console.WriteLine("0. Back");
            Console.Write("\nChoose an option: ");

            var opt = Console.ReadLine();
            switch (opt)
            {
                case "1":
                    Schedule(svc, patientSvc, doctorSvc);
                    break;
                case "2":
                    ListByPatient(svc, patientSvc);
                    break;
                case "3":
                    ListByDoctor(svc, doctorSvc);
                    break;
                case "4":
                    Cancel(svc);
                    break;
                case "5":
                    Complete(svc);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option. Press any key...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void Schedule(IAppointmentService svc, IPatientService patientSvc, IDoctorService doctorSvc)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("=== Schedule Appointment ===");
            Console.WriteLine("Available Patients:");
            foreach (var p in patientSvc.GetAll())
                Console.WriteLine($"{p.Id} - {p.Name} (Doc {p.DocumentNumber})");

            Console.WriteLine("\nAvailable Doctors:");
            foreach (var d in doctorSvc.GetAll())
                Console.WriteLine($"{d.Id} - {d.Name} ({d.Specialty})");

            var patientIdStr = ConsoleHelper.ReadString("Patient Id (GUID): ");
            var doctorIdStr = ConsoleHelper.ReadString("Doctor Id (GUID): ");
            var dateStr = ConsoleHelper.ReadString("Date and time (yyyy-MM-dd HH:mm): ");

            if (!Guid.TryParse(patientIdStr, out var patientId) || !Guid.TryParse(doctorIdStr, out var doctorId))
            {
                Console.WriteLine("Invalid GUID(s). Press any key...");
                Console.ReadKey();
                return;
            }

            if (!DateTime.TryParseExact(dateStr, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out var scheduledAt))
            {
                Console.WriteLine("Invalid date format. Press any key...");
                Console.ReadKey();
                return;
            }

            svc.Schedule(patientId, doctorId, scheduledAt);
            Console.WriteLine("Appointment scheduled. (A confirmation email will be sent if configured).");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key...");
        Console.ReadKey();
    }

    private void ListByPatient(IAppointmentService svc, IPatientService patientSvc)
    {
        Console.Clear();
        Console.WriteLine("=== List Appointments by Patient ===");
        var doc = ConsoleHelper.ReadInt("Patient document number: ");
        var patient = patientSvc.GetByDocument(doc);
        if (patient == null)
        {
            Console.WriteLine("Patient not found.");
            Console.ReadKey();
            return;
        }

        var list = svc.GetByPatient(patient.Id);
        if (!list.Any()) Console.WriteLine("No appointments for this patient.");
        else
        {
            foreach (var a in list)
            {
                Console.WriteLine($"Id: {a.Id} | DoctorId: {a.DoctorId} | Date: {a.ScheduledAt} | Status: {a.Status}");
            }
        }

        Console.WriteLine("Press any key...");
        Console.ReadKey();
    }

    private void ListByDoctor(IAppointmentService svc, IDoctorService doctorSvc)
    {
        Console.Clear();
        Console.WriteLine("=== List Appointments by Doctor ===");
        var doc = ConsoleHelper.ReadInt("Doctor document number: ");
        var doctor = doctorSvc.GetAll().FirstOrDefault(d => d.DocumentNumber == doc);
        if (doctor == null)
        {
            Console.WriteLine("Doctor not found.");
            Console.ReadKey();
            return;
        }

        var list = svc.GetByDoctor(doctor.Id);
        if (!list.Any()) Console.WriteLine("No appointments for this doctor.");
        else
        {
            foreach (var a in list)
            {
                Console.WriteLine($"Id: {a.Id} | PatientId: {a.PatientId} | Date: {a.ScheduledAt} | Status: {a.Status}");
            }
        }

        Console.WriteLine("Press any key...");
        Console.ReadKey();
    }

    private void Cancel(IAppointmentService svc)
    {
        Console.Clear();
        var idStr = ConsoleHelper.ReadString("Appointment Id (GUID): ");
        if (!Guid.TryParse(idStr, out var id))
        {
            Console.WriteLine("Invalid GUID. Press any key...");
            Console.ReadKey();
            return;
        }

        try
        {
            svc.Cancel(id);
            Console.WriteLine("Appointment cancelled.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key...");
        Console.ReadKey();
    }

    private void Complete(IAppointmentService svc)
    {
        Console.Clear();
        var idStr = ConsoleHelper.ReadString("Appointment Id (GUID): ");
        if (!Guid.TryParse(idStr, out var id))
        {
            Console.WriteLine("Invalid GUID. Press any key...");
            Console.ReadKey();
            return;
        }

        try
        {
            svc.Complete(id);
            Console.WriteLine("Appointment marked as completed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key...");
        Console.ReadKey();
    }
}
