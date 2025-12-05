namespace Refill.Domain;

public class Medication
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string NdcCode { get; private set; } = string.Empty; // US national drug code
    public string Name { get; private set; } = string.Empty;
    public bool IsControlledSubstance { get; private set; }
    public int MaxRefillsAllowed { get; private set; }
    public int RefillWindowDays { get; private set; } // how many days before running out patient may refill

    private Medication() { }

    public Medication(string ndcCode, string name, bool isControlled, int maxRefills, int refillWindowDays)
    {
        if (string.IsNullOrWhiteSpace(ndcCode)) throw new ArgumentException("NDC required", nameof(ndcCode));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required", nameof(name));
        if (maxRefills < 0) throw new ArgumentOutOfRangeException(nameof(maxRefills));
        if (refillWindowDays <= 0) throw new ArgumentOutOfRangeException(nameof(refillWindowDays));

        NdcCode = ndcCode;
        Name = name;
        IsControlledSubstance = isControlled;
        MaxRefillsAllowed = maxRefills;
        RefillWindowDays = refillWindowDays;
    }
}
