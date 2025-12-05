namespace Refill.Domain;

public class Patient
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string WalgreensId { get; private set; } = string.Empty; // internal Walgreens patient ID
    public string FullName { get; private set; } = string.Empty;
    public DateTime DateOfBirth { get; private set; }
    public string PhoneNumber { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public bool IsBlockedForFraud { get; private set; }

    private Patient() { }

    public Patient(string walgreensId, string fullName, DateTime dob, string phone, string email)
    {
        if (string.IsNullOrWhiteSpace(walgreensId)) throw new ArgumentException("Walgreens ID is required", nameof(walgreensId));
        if (string.IsNullOrWhiteSpace(fullName)) throw new ArgumentException("Name is required", nameof(fullName));

        WalgreensId = walgreensId;
        FullName = fullName;
        DateOfBirth = dob;
        PhoneNumber = phone;
        Email = email;
    }

    public void BlockForFraud() => IsBlockedForFraud = true;
}
