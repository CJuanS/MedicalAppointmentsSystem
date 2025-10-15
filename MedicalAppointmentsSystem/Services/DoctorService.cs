using patient_management_system.Data;
using patient_management_system.Models;
using patient_management_system.Interfaces; 
using Microsoft.EntityFrameworkCore;

namespace patient_management_system.Services;

public class DoctorService : IDoctorService
{
    private readonly AppDbContext _context;

    public DoctorService(AppDbContext context)
    {
        _context = context;
    }

    public void Add(Doctor doctor)
    {
        if (doctor == null) throw new ArgumentNullException(nameof(doctor));
        if (doctor.DocumentNumber <= 0) throw new ArgumentException("Document number is required.");

        if (_context.Doctors.Any(d => d.DocumentNumber == doctor.DocumentNumber))
            throw new Exception("Document already exists.");

        // optional: check combination name + specialty
        if (_context.Doctors.Any(d => d.Name == doctor.Name && d.Specialty == doctor.Specialty))
            throw new Exception("A doctor with same name and specialty already exists.");

        _context.Doctors.Add(doctor);
        _context.SaveChanges();
    }

    public void Update(Doctor doctor)
    {
        var existing = _context.Doctors.Find(doctor.Id);
        if (existing == null) throw new Exception("Doctor not found.");

        existing.Name = doctor.Name;
        existing.Email = doctor.Email;
        existing.Phone = doctor.Phone;
        existing.Specialty = doctor.Specialty;
        _context.SaveChanges();
    }

    public List<Doctor> GetAll() => _context.Doctors.AsNoTracking().ToList();

    public List<Doctor> GetBySpecialty(string specialty)
        => _context.Doctors.AsNoTracking().Where(d => d.Specialty == specialty).ToList();
}
