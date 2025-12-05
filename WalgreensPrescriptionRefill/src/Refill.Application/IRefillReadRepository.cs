using Refill.Domain;

namespace Refill.Application;

public interface IRefillReadRepository
{
    Task<Patient?> GetPatientByWalgreensIdAsync(string walgreensId, CancellationToken ct = default);
    Task<Medication?> GetMedicationByNdcAsync(string ndcCode, CancellationToken ct = default);
    Task<Prescription?> GetPrescriptionByRxNumberAsync(string rxNumber, CancellationToken ct = default);
    Task<IReadOnlyList<PharmacyStore>> GetStoresByZipAsync(string zipCode, CancellationToken ct = default);
    Task<InventoryItem?> GetInventoryAsync(Guid storeId, Guid medicationId, CancellationToken ct = default);
}
