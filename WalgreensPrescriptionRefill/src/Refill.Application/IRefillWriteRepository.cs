using Refill.Domain;

namespace Refill.Application;

public interface IRefillWriteRepository
{
    Task AddRefillRequestAsync(RefillRequest request, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
