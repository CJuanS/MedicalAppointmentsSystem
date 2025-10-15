using patient_management_system.Data;
using patient_management_system.Models;
using patient_management_system.Interfaces;  
using Microsoft.EntityFrameworkCore;

namespace patient_management_system.Services;

public class PatientService : IPatientService
{
    private readonly AppDbContext _context;

    public PatientService(AppDbContext context)
    {
        _context = context;
    }

    public void Create(Patient patient)
    {
        if (patient == null) throw new ArgumentNullException(nameof(patient));
        if (patient.DocumentNumber <= 0) throw new ArgumentException("Document number is required.");

        if (_context.Patients.Any(p => p.DocumentNumber == patient.DocumentNumber))
            throw new Exception("Patient with same document already exists.");

        _context.Patients.Add(patient);
        _context.SaveChanges();
    }

    public void Update(Patient patient)
    {
        var existing = _context.Patients.Find(patient.Id);
        if (existing == null) throw new Exception("Patient not found.");

        existing.Name = patient.Name;
        existing.Email = patient.Email;
        existing.Phone = patient.Phone;
        existing.Age = patient.Age;
        // DocumentNumber should usually not change; if needed add validation
        _context.SaveChanges();
    }

    public List<Patient> GetAll() => _context.Patients.AsNoTracking().ToList();

    public Patient? GetByDocument(int documentNumber)
        => _context.Patients.AsNoTracking().FirstOrDefault(p => p.DocumentNumber == documentNumber);

    public Patient? GetById(Guid id)
        => _context.Patients.Find(id);

    public bool ValidateUniqueDocument(int documentNumber)
        => !_context.Patients.Any(p => p.DocumentNumber == documentNumber);
}
