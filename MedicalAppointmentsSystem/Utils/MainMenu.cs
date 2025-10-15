using patient_management_system.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace patient_management_system.Menus;

public class MainMenu
{
    private readonly IServiceProvider _provider;

    public MainMenu(IServiceProvider provider)
    {
        _provider = provider;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Medical Appointments System ===");
            Console.WriteLine("1. Patients");
            Console.WriteLine("2. Doctors");
            Console.WriteLine("3. Appointments");
            Console.WriteLine("0. Exit");
            Console.Write("\nChoose an option: ");

            var key = Console.ReadLine();
            switch (key)
            {
                case "1":
                    var patientMenu = new PatientMenu(_provider);
                    patientMenu.Show();
                    break;
                case "2":
                    var doctorMenu = new DoctorMenu(_provider);
                    doctorMenu.Show();
                    break;
                case "3":
                    var appointmentMenu = new AppointmentMenu(_provider);
                    appointmentMenu.Show();
                    break;
                case "0":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
