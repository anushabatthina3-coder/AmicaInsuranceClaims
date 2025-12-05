namespace Refill.Application;

public record MedicationDto(Guid Id, string NdcCode, string Name, bool IsControlledSubstance);
public record StoreAvailabilityDto(string StoreNumber, string City, string State, bool Is24Hour, bool HasStock);
public record RefillEligibilityResultDto(
    bool IsEligible,
    string Reason,
    DateTime? EarliestFillDate,
    int RefillsRemaining,
    bool IsControlledSubstance);
