using Microsoft.EntityFrameworkCore;
using Refill.Domain;

namespace Refill.Infrastructure;

public class RefillDbContext : DbContext
{
    public RefillDbContext(DbContextOptions<RefillDbContext> options) : base(options) { }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<PharmacyStore> Stores => Set<PharmacyStore>();
    public DbSet<Medication> Medications => Set<Medication>();
    public DbSet<Prescription> Prescriptions => Set<Prescription>();
    public DbSet<InventoryItem> Inventory => Set<InventoryItem>();
    public DbSet<RefillRequest> RefillRequests => Set<RefillRequest>();
}
