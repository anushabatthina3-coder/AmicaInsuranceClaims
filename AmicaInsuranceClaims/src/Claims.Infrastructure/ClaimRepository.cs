using Claims.Application;
using Claims.Domain;
using Microsoft.EntityFrameworkCore;
namespace Claims.Infrastructure;
public class ClaimRepository:IClaimRepository{
 private readonly ClaimsDb _db;
 public ClaimRepository(ClaimsDb db)=>_db=db;
 public async Task AddAsync(Claim c)=>await _db.Claims.AddAsync(c);
 public Task<Claim?> GetAsync(Guid id)=>_db.Claims.FirstOrDefaultAsync(x=>x.Id==id).AsTask();
 public Task<List<Claim>> GetAllAsync()=>_db.Claims.ToListAsync();
 public Task SaveAsync()=>_db.SaveChangesAsync();
}
