using patient_management_system.Interfaces;
using patient_management_system.Models;
using Microsoft.Extensions.DependencyInjection;
using patient_management_system.Utils;

namespace patient_management_system.Menus;

public class PatientMenu
{
    private readonly IServiceProvider _provider;

    public PatientMenu(IServiceProvider provider)
    {
        _provider = provider;
    }

    public void Show()
    {
        var svc = _provider.CreateScope().ServiceProvider.GetRequiredService<IPatientService>();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Patients Menu ===");
            Console.WriteLine("1. Create patient");
            Console.WriteLine("2. List patients");
            Console.WriteLine("3. Update patient");
            Console.WriteLine("0. Back");
            Console.Write("\nChoose an option: ");

            var opt = Console.ReadLine();
            switch (opt)
            {
                case "1":
                    CreatePatient(svc);
                    break;
                case "2":
                    ListPatients(svc);
                    break;
                case "3":
                    UpdatePatient(svc);
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

    private void CreatePatient(IPatientService svc)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("=== Create Patient ===");
            var name = ConsoleHelper.ReadString("Name: ");
            var doc = ConsoleHelper.ReadInt("Document number: ");
            var age = ConsoleHelper.ReadInt("Age: ");
            var phone = ConsoleHelper.ReadInt("Phone: ");
            var email = ConsoleHelper.ReadString("Email: ");

            var patient = new Patient
            {
                Name = name,
                DocumentNumber = doc,
                Age = age,
                Phone = phone,
                Email = email
            };

            svc.Create(patient);
            Console.WriteLine("Patient created successfully. Press any key...");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Console.ReadKey();
    }

    private void ListPatients(IPatientService svc)
    {
        Console.Clear();
        Console.WriteLine("=== Patients List ===");
        var list = svc.GetAll();
        if (!list.Any())
        {
            Console.WriteLine("No patients found.");
        }
        else
        {
            foreach (var p in list)
            {
                Console.WriteLine($"Id: {p.Id} | Name: {p.Name} | Doc: {p.DocumentNumber} | Age: {p.Age} | Phone: {p.Phone} | Email: {p.Email}");
            }
        }
        Console.WriteLine("Press any key...");
        Console.ReadKey();
    }

    private void UpdatePatient(IPatientService svc)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("=== Update Patient ===");
            var idStr = ConsoleHelper.ReadString("Patient Id (GUID): ");
            if (!Guid.TryParse(idStr, out var id))
            {
                Console.WriteLine("Invalid GUID. Press any key...");
                Console.ReadKey();
                return;
            }

            var existing = svc.GetById(id);
            if (existing == null)
            {
                Console.WriteLine("Patient not found. Press any key...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Updating patient: {existing.Name} (Doc: {existing.DocumentNumber})");
            var newName = ConsoleHelper.ReadString($"Name ({existing.Name}): ", allowEmpty: true);
            var newAge = ConsoleHelper.ReadInt($"Age ({existing.Age}): ", allowEmpty: true);
            var newPhone = ConsoleHelper.ReadInt($"Phone ({existing.Phone}): ", allowEmpty: true);
            var newEmail = ConsoleHelper.ReadString($"Email ({existing.Email}): ", allowEmpty: true);

            if (!string.IsNullOrWhiteSpace(newName))
                existing.Name = newName;
            if (newAge != int.MinValue)
                existing.Age = newAge;
            if (newPhone != int.MinValue)
                existing.Phone = newPhone;
            if (!string.IsNullOrWhiteSpace(newEmail))
                existing.Email = newEmail;

            svc.Update(existing);
            Console.WriteLine("Patient updated. Press any key...");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.ReadKey();
    }
}
