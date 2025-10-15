using patient_management_system.Models;

namespace patient_management_system.Interfaces;

public interface IDoctorService
{
    void Add(Doctor doctor);
    void Update(Doctor doctor);
    List<Doctor> GetAll();
    List<Doctor> GetBySpecialty(string specialty);
}
