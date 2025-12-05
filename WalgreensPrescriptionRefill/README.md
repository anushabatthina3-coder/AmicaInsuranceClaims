# Walgreens Prescription Refill & Real-Time Availability API (Demo)

This is a .NET 8 demo solution that represents an enterprise-style **Walgreens Prescription Refill & Real-Time Drug Availability** back-end.

The system models:

- Patients and fraud blocks
- Walgreens pharmacy stores
- Medications and refill rules
- Prescriptions (days supply, refills remaining)
- Store inventory and safety stock
- Refill requests and approval logic

## Solution Structure

- **Refill.Domain** – core entities (`Patient`, `PharmacyStore`, `Medication`, `Prescription`, `InventoryItem`, `RefillRequest`)
- **Refill.Application** – DTOs, `RefillEligibilityService`, `RefillExecutionService`, repository interfaces
- **Refill.Infrastructure** – EF Core `RefillDbContext`, combined read/write repository
- **Refill.API** – minimal API exposing:
  - `GET /api/refill/eligibility` – evaluate whether a given RX is eligible to refill
  - `POST /api/refill` – place a refill request and simulate store inventory allocation

The API uses an in-memory database and seeds demo data so it can run without external dependencies.

> This project is for portfolio / learning purposes only and is not affiliated with Walgreens.
