using Refill.Domain;

namespace Refill.Application;

public class RefillEligibilityService
{
    private readonly IRefillReadRepository _readRepo;

    public RefillEligibilityService(IRefillReadRepository readRepo)
    {
        _readRepo = readRepo;
    }

    public async Task<RefillEligibilityResultDto> EvaluateEligibilityAsync(
        string patientWalgreensId,
        string rxNumber,
        DateTime asOfDate,
        CancellationToken ct = default)
    {
        var patient = await _readRepo.GetPatientByWalgreensIdAsync(patientWalgreensId, ct);
        if (patient is null)
        {
            return new RefillEligibilityResultDto(false, "PATIENT_NOT_FOUND", null, 0, false);
        }

        if (patient.IsBlockedForFraud)
        {
            return new RefillEligibilityResultDto(false, "PATIENT_BLOCKED_FOR_FRAUD", null, 0, false);
        }

        var prescription = await _readRepo.GetPrescriptionByRxNumberAsync(rxNumber, ct);
        if (prescription is null)
        {
            return new RefillEligibilityResultDto(false, "PRESCRIPTION_NOT_FOUND", null, 0, false);
        }

        if (prescription.PatientId != patient.Id)
        {
            return new RefillEligibilityResultDto(false, "RX_DOES_NOT_BELONG_TO_PATIENT", null, 0, false);
        }

        if (prescription.IsCancelled)
        {
            return new RefillEligibilityResultDto(false, "PRESCRIPTION_CANCELLED", null, 0, false);
        }

        if (prescription.RefillsRemaining <= 0)
        {
            return new RefillEligibilityResultDto(false, "NO_REFILLS_REMAINING", null, 0, false);
        }

        var medication = await _readRepo.GetMedicationByNdcAsync(
            ndcCode: string.Empty /* this demo assumes medication already referenced by Id */,
            ct: ct);

        // Compute refill window: patient can refill if within allowed days window before running out
        var nextRunOutDate = prescription.LastFillDate.AddDays(prescription.DaysSupply);
        var earliestFillDate = nextRunOutDate.AddDays(-7); // 7-day early refill window

        if (asOfDate < earliestFillDate)
        {
            return new RefillEligibilityResultDto(
                false,
                "TOO_EARLY_TO_REFILL",
                earliestFillDate,
                prescription.RefillsRemaining,
                medication?.IsControlledSubstance ?? false);
        }

        return new RefillEligibilityResultDto(
            true,
            "ELIGIBLE",
            earliestFillDate,
            prescription.RefillsRemaining,
            medication?.IsControlledSubstance ?? false);
    }
}
