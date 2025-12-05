namespace Refill.Domain;

public class Prescription
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string RxNumber { get; private set; } = string.Empty;
    public Guid PatientId { get; private set; }
    public Guid MedicationId { get; private set; }
    public int QuantityPerFill { get; private set; }
    public int DaysSupply { get; private set; }
    public int RefillsRemaining { get; private set; }
    public DateTime LastFillDate { get; private set; }
    public bool IsCancelled { get; private set; }

    private Prescription() { }

    public Prescription(string rxNumber, Guid patientId, Guid medicationId, int quantityPerFill, int daysSupply, int refillsRemaining, DateTime lastFillDate)
    {
        if (string.IsNullOrWhiteSpace(rxNumber)) throw new ArgumentException("Rx number required", nameof(rxNumber));
        if (quantityPerFill <= 0) throw new ArgumentOutOfRangeException(nameof(quantityPerFill));
        if (daysSupply <= 0) throw new ArgumentOutOfRangeException(nameof(daysSupply));
        if (refillsRemaining < 0) throw new ArgumentOutOfRangeException(nameof(refillsRemaining));

        RxNumber = rxNumber;
        PatientId = patientId;
        MedicationId = medicationId;
        QuantityPerFill = quantityPerFill;
        DaysSupply = daysSupply;
        RefillsRemaining = refillsRemaining;
        LastFillDate = lastFillDate;
    }

    public void Cancel() => IsCancelled = true;

    public void RegisterFill(DateTime fillDate)
    {
        if (RefillsRemaining <= 0)
            throw new InvalidOperationException("No refills remaining");
        if (IsCancelled)
            throw new InvalidOperationException("Prescription is cancelled");
        if (fillDate < LastFillDate)
            throw new ArgumentException("Fill date cannot be before last fill", nameof(fillDate));

        RefillsRemaining -= 1;
        LastFillDate = fillDate;
    }
}
