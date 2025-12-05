using Claims.Domain;
namespace Claims.Application;
public interface IClaimRepository{
 Task AddAsync(Claim c);
 Task<Claim?> GetAsync(Guid id);
 Task<List<Claim>> GetAllAsync();
 Task SaveAsync();
}
