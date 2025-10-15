namespace patient_management_system.Models;

public class Patient : Person
{
    public int Age { get; set; }

    public void UpdateInfo(int age, int phone, string email)
    {
        Age = age;
        Phone = phone;
        Email = email;
    }

    public bool ValidateUniquePatient()
    {
        // La validación real se hará en PatientService
        return true;
    }

    public List<Appointment> GetAppointments()
    {
        return new List<Appointment>();
    }
}
