using Refill.Domain;

namespace Refill.Application;

public class RefillExecutionService
{
    private readonly IRefillReadRepository _readRepo;
    private readonly IRefillWriteRepository _writeRepo;

    public RefillExecutionService(IRefillReadRepository readRepo, IRefillWriteRepository writeRepo)
    {
        _readRepo = readRepo;
        _writeRepo = writeRepo;
    }

    public async Task<(RefillRequest Request, string Status)> PlaceRefillAsync(
        string patientWalgreensId,
        string rxNumber,
        string preferredStoreNumber,
        CancellationToken ct = default)
    {
        var patient = await _readRepo.GetPatientByWalgreensIdAsync(patientWalgreensId, ct)
                      ?? throw new InvalidOperationException("Patient not found");
        var prescription = await _readRepo.GetPrescriptionByRxNumberAsync(rxNumber, ct)
                          ?? throw new InvalidOperationException("Prescription not found");

        var eligibilityService = new RefillEligibilityService(_readRepo);
        var eligibility = await eligibilityService.EvaluateEligibilityAsync(patientWalgreensId, rxNumber, DateTime.UtcNow, ct);
        if (!eligibility.IsEligible)
        {
            throw new InvalidOperationException($"Refill not eligible: {eligibility.Reason}");
        }

        var stores = await _readRepo.GetStoresByZipAsync("00000", ct); // simplified for demo
        var store = stores.First(); // assume first store
        var inventory = await _readRepo.GetInventoryAsync(store.Id, prescription.MedicationId, ct);
        if (inventory is null || !inventory.HasSufficientStock(prescription.QuantityPerFill))
        {
            throw new InvalidOperationException("Store does not have sufficient inventory.");
        }

        inventory.Deduct(prescription.QuantityPerFill);

        var refillRequest = new RefillRequest(prescription.Id, store.Id);
        refillRequest.Approve();

        await _writeRepo.AddRefillRequestAsync(refillRequest, ct);
        await _writeRepo.SaveChangesAsync(ct);

        prescription.RegisterFill(DateTime.UtcNow);

        return (refillRequest, "APPROVED");
    }
}
