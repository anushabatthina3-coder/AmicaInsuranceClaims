namespace Refill.Domain;

public class PharmacyStore
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string StoreNumber { get; private set; } = string.Empty; // Walgreens store #12345
    public string City { get; private set; } = string.Empty;
    public string State { get; private set; } = string.Empty;
    public string ZipCode { get; private set; } = string.Empty;
    public bool Is24Hour { get; private set; }

    private PharmacyStore() { }

    public PharmacyStore(string storeNumber, string city, string state, string zip, bool is24Hour)
    {
        StoreNumber = storeNumber;
        City = city;
        State = state;
        ZipCode = zip;
        Is24Hour = is24Hour;
    }
}
