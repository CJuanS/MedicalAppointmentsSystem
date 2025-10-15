using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using patient_management_system.Data;
using patient_management_system.Interfaces;
using patient_management_system.Models;
using patient_management_system.Services;

//la contraseña del usuario postgres, reemplázala en Password=
var connectionString = "Host=localhost;Port=5432;Database=MedicalAppointmentsDB;Username=postgres;Password=12345";

// Configurar los servicios de inyección de dependencias (DI)
var services = new ServiceCollection();

// Registrar el contexto de base de datos usando PostgreSQL
services.AddDbContext<AppDbContext>(options =>
    options
        .UseNpgsql(connectionString)
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine, LogLevel.Error));


// Registrar los servicios de negocio
services.AddScoped<IPatientService, PatientService>();
services.AddScoped<IDoctorService, DoctorService>();
services.AddScoped<IAppointmentService, AppointmentService>();
services.AddScoped<IEmailService, EmailService>();

// Construir el proveedor de servicios
var provider = services.BuildServiceProvider();

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("🚀 Medical Appointments System — Console + EF Core");
Console.ResetColor();

try
{
    var mainMenu = new patient_management_system.Menus.MainMenu(provider);
    mainMenu.Show();

}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Error: " + ex.Message);

    var inner = ex.InnerException;
    while (inner != null)
    {
        Console.WriteLine("Inner exception: " + inner.Message);
        inner = inner.InnerException;
    }

    Console.ResetColor();
}



Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();
