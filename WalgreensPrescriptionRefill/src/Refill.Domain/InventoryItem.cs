namespace Refill.Domain;

public class InventoryItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid StoreId { get; private set; }
    public Guid MedicationId { get; private set; }
    public int OnHandQuantity { get; private set; }
    public int SafetyStock { get; private set; }

    private InventoryItem() { }

    public InventoryItem(Guid storeId, Guid medicationId, int onHandQuantity, int safetyStock)
    {
        StoreId = storeId;
        MedicationId = medicationId;
        OnHandQuantity = onHandQuantity;
        SafetyStock = safetyStock;
    }

    public bool HasSufficientStock(int requestedQty) =>
        OnHandQuantity - requestedQty >= SafetyStock;

    public void Deduct(int qty)
    {
        if (!HasSufficientStock(qty))
            throw new InvalidOperationException("Insufficient inventory for requested quantity.");
        OnHandQuantity -= qty;
    }
}
