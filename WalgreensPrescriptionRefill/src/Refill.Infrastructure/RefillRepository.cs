using Microsoft.EntityFrameworkCore;
using Refill.Application;
using Refill.Domain;

namespace Refill.Infrastructure;

public class RefillRepository : IRefillReadRepository, IRefillWriteRepository
{
    private readonly RefillDbContext _db;

    public RefillRepository(RefillDbContext db)
    {
        _db = db;
    }

    public Task<Patient?> GetPatientByWalgreensIdAsync(string walgreensId, CancellationToken ct = default) =>
        _db.Patients.FirstOrDefaultAsync(p => p.WalgreensId == walgreensId, ct);

    public Task<Medication?> GetMedicationByNdcAsync(string ndcCode, CancellationToken ct = default) =>
        _db.Medications.FirstOrDefaultAsync(m => m.NdcCode == ndcCode, ct);

    public Task<Prescription?> GetPrescriptionByRxNumberAsync(string rxNumber, CancellationToken ct = default) =>
        _db.Prescriptions.FirstOrDefaultAsync(p => p.RxNumber == rxNumber, ct);

    public async Task<IReadOnlyList<PharmacyStore>> GetStoresByZipAsync(string zipCode, CancellationToken ct = default)
    {
        return await _db.Stores.ToListAsync(ct); // simplified for demo
    }

    public Task<InventoryItem?> GetInventoryAsync(Guid storeId, Guid medicationId, CancellationToken ct = default) =>
        _db.Inventory.FirstOrDefaultAsync(i => i.StoreId == storeId && i.MedicationId == medicationId, ct);

    public async Task AddRefillRequestAsync(RefillRequest request, CancellationToken ct = default)
    {
        await _db.RefillRequests.AddAsync(request, ct);
    }

    public Task SaveChangesAsync(CancellationToken ct = default) =>
        _db.SaveChangesAsync(ct);
}
