namespace Claims.Application;
public record ClaimDto(Guid Id,string PolicyNumber,string CustomerName,decimal Amount,string Status,string? AdjusterNotes);
