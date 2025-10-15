using patient_management_system.Interfaces;
using patient_management_system.Models;
using Microsoft.Extensions.DependencyInjection;
using patient_management_system.Utils;

namespace patient_management_system.Menus;

public class DoctorMenu
{
    private readonly IServiceProvider _provider;

    public DoctorMenu(IServiceProvider provider)
    {
        _provider = provider;
    }

    public void Show()
    {
        var svc = _provider.CreateScope().ServiceProvider.GetRequiredService<IDoctorService>();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Doctors Menu ===");
            Console.WriteLine("1. Create doctor");
            Console.WriteLine("2. List doctors");
            Console.WriteLine("3. Filter by specialty");
            Console.WriteLine("4. Update doctor");
            Console.WriteLine("0. Back");
            Console.Write("\nChoose an option: ");

            var opt = Console.ReadLine();
            switch (opt)
            {
                case "1":
                    CreateDoctor(svc);
                    break;
                case "2":
                    ListDoctors(svc);
                    break;
                case "3":
                    FilterBySpecialty(svc);
                    break;
                case "4":
                    UpdateDoctor(svc);
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

    private void CreateDoctor(IDoctorService svc)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("=== Create Doctor ===");
            var name = ConsoleHelper.ReadString("Name: ");
            var doc = ConsoleHelper.ReadInt("Document number: ");
            var specialty = ConsoleHelper.ReadString("Specialty: ");
            var phone = ConsoleHelper.ReadInt("Phone: ");
            var email = ConsoleHelper.ReadString("Email: ");

            var doctor = new Doctor
            {
                Name = name,
                DocumentNumber = doc,
                Specialty = specialty,
                Phone = phone,
                Email = email
            };

            svc.Add(doctor);
            Console.WriteLine("Doctor created successfully. Press any key...");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Console.ReadKey();
    }

    private void ListDoctors(IDoctorService svc)
    {
        Console.Clear();
        Console.WriteLine("=== Doctors List ===");
        var list = svc.GetAll();
        if (!list.Any()) Console.WriteLine("No doctors found.");
        else
        {
            foreach (var d in list)
            {
                Console.WriteLine($"Id: {d.Id} | Name: {d.Name} | Specialty: {d.Specialty} | Doc: {d.DocumentNumber} | Phone: {d.Phone} | Email: {d.Email}");
            }
        }
        Console.WriteLine("Press any key...");
        Console.ReadKey();
    }

    private void FilterBySpecialty(IDoctorService svc)
    {
        Console.Clear();
        var specialty = ConsoleHelper.ReadString("Specialty to filter: ");
        var list = svc.GetBySpecialty(specialty);
        Console.WriteLine($"=== Doctors with specialty '{specialty}' ===");
        if (!list.Any()) Console.WriteLine("No doctors found.");
        else
        {
            foreach (var d in list)
                Console.WriteLine($"Id: {d.Id} | {d.Name} — Doc: {d.DocumentNumber}");
        }
        Console.WriteLine("Press any key...");
        Console.ReadKey();
    }

    private void UpdateDoctor(IDoctorService svc)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("=== Update Doctor ===");
            var idStr = ConsoleHelper.ReadString("Doctor Id (GUID): ");
            if (!Guid.TryParse(idStr, out var id))
            {
                Console.WriteLine("Invalid GUID. Press any key...");
                Console.ReadKey();
                return;
            }

            var existing = svc.GetAll().FirstOrDefault(d => d.Id == id);
            if (existing == null)
            {
                Console.WriteLine("Doctor not found. Press any key...");
                Console.ReadKey();
                return;
            }

            var newName = ConsoleHelper.ReadString($"Name ({existing.Name}): ", allowEmpty: true);
            var newSpecialty = ConsoleHelper.ReadString($"Specialty ({existing.Specialty}): ", allowEmpty: true);
            var newPhone = ConsoleHelper.ReadInt($"Phone ({existing.Phone}): ", allowEmpty: true);
            var newEmail = ConsoleHelper.ReadString($"Email ({existing.Email}): ", allowEmpty: true);

            if (!string.IsNullOrWhiteSpace(newName)) existing.Name = newName;
            if (!string.IsNullOrWhiteSpace(newSpecialty)) existing.Specialty = newSpecialty;
            if (newPhone != int.MinValue) existing.Phone = newPhone;
            if (!string.IsNullOrWhiteSpace(newEmail)) existing.Email = newEmail;

            svc.Update(existing);
            Console.WriteLine("Doctor updated. Press any key...");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Console.ReadKey();
    }
}
