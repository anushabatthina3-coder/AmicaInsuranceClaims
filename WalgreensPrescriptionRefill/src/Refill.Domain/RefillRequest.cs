namespace Refill.Domain;

public enum RefillStatus
{
    Pending = 1,
    Approved = 2,
    Rejected = 3
}

public class RefillRequest
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid PrescriptionId { get; private set; }
    public Guid StoreId { get; private set; }
    public DateTime RequestedAtUtc { get; private set; }
    public RefillStatus Status { get; private set; }
    public string? RejectionReason { get; private set; }

    private RefillRequest() { }

    public RefillRequest(Guid prescriptionId, Guid storeId)
    {
        PrescriptionId = prescriptionId;
        StoreId = storeId;
        RequestedAtUtc = DateTime.UtcNow;
        Status = RefillStatus.Pending;
    }

    public void Approve() =>
        Status = RefillStatus.Approved;

    public void Reject(string reason)
    {
        Status = RefillStatus.Rejected;
        RejectionReason = reason;
    }
}
