namespace patient_management_system.Models;

public class Doctor : Person
{
    public string Specialty { get; set; } = string.Empty;

    public void AssignSpecialty(string specialty)
    {
        Specialty = specialty;
    }
    public bool ValidateUniqueDoctor()
    {
        // Validación real en DoctorService
        return true;
    }

    public List<Appointment> GetAppointments()
    {
        return new List<Appointment>();
    }
}
