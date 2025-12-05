using Microsoft.EntityFrameworkCore;
using Refill.Application;
using Refill.Infrastructure;
using Refill.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RefillDbContext>(options =>
    options.UseInMemoryDatabase("WalgreensRefill"));

builder.Services.AddScoped<IRefillReadRepository, RefillRepository>();
builder.Services.AddScoped<IRefillWriteRepository, RefillRepository>();
builder.Services.AddScoped<RefillEligibilityService>();
builder.Services.AddScoped<RefillExecutionService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// seed demo data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RefillDbContext>();

    if (!db.Patients.Any())
    {
        var patient = new Patient("WG123456", "Jane Doe", new DateTime(1989, 5, 1), "555-1234", "jane@example.com");
        db.Patients.Add(patient);

        var store = new PharmacyStore("WAL-03421", "Chicago", "IL", "60601", true);
        db.Stores.Add(store);

        var med = new Medication("0002-8215", "Atorvastatin 20mg", false, maxRefills: 5, refillWindowDays: 7);
        db.Medications.Add(med);

        var rx = new Prescription("RX100200", patient.Id, med.Id, quantityPerFill: 30, daysSupply: 30, refillsRemaining: 3, lastFillDate: DateTime.UtcNow.AddDays(-25));
        db.Prescriptions.Add(rx);

        var inv = new InventoryItem(store.Id, med.Id, onHandQuantity: 500, safetyStock: 50);
        db.Inventory.Add(inv);

        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/refill/eligibility", async (
    string patientWalgreensId,
    string rxNumber,
    DateTime? asOfDate,
    RefillEligibilityService service,
    CancellationToken ct) =>
{
    var result = await service.EvaluateEligibilityAsync(
        patientWalgreensId,
        rxNumber,
        asOfDate ?? DateTime.UtcNow,
        ct);

    return Results.Ok(result);
});

app.MapPost("/api/refill", async (
    RefillRequestCommand command,
    RefillExecutionService service,
    CancellationToken ct) =>
{
    var (request, status) = await service.PlaceRefillAsync(
        command.PatientWalgreensId,
        command.RxNumber,
        command.PreferredStoreNumber,
        ct);

    return Results.Ok(new { request.Id, request.Status, status });
});

app.Run();

public record RefillRequestCommand(string PatientWalgreensId, string RxNumber, string PreferredStoreNumber);
