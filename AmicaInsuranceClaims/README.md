# Amica Insurance Claim Management System (.NET 8)

A professional 6+ years–level enterprise project modeling Amica Insurance claim intake, review workflow, adjuster notes, and decision logic.

## Layers
- **Claims.Domain** – Claim, Policy, ClaimStatus
- **Claims.Application** – ClaimService, DTOs, business rules
- **Claims.Infrastructure** – EF Core DbContext, ClaimRepository
- **Claims.API** – Minimal API with Swagger

## Endpoints
- POST `/api/claims` – submit a claim  
- GET `/api/claims` – list all  
- GET `/api/claims/{id}` – get one  
- POST `/api/claims/{id}/review` – approve/reject  

Uses in-memory DB for easy running and deployment.
