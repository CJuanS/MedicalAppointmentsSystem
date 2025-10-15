namespace patient_management_system.Models;

public class Person
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public int DocumentNumber { get; set; }
    public int Phone { get; set; }
    public string Email { get; set; } = string.Empty;

    public bool ValidateDocument()
    {
        // Placeholder: la validación real estará en el servicio
        return DocumentNumber > 0;
    }

    public void UpdateContact(int phone, string email)
    {
        Phone = phone;
        Email = email;
    }
}
