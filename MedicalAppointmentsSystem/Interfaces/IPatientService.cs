using patient_management_system.Models;

namespace patient_management_system.Interfaces;

public interface IPatientService
{
    void Create(Patient patient);
    void Update(Patient patient);
    List<Patient> GetAll();
    Patient? GetByDocument(int documentNumber);
    bool ValidateUniqueDocument(int documentNumber);
    Patient? GetById(Guid id);
}
