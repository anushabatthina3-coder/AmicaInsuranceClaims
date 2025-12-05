using Microsoft.EntityFrameworkCore;
using Claims.Domain;
namespace Claims.Infrastructure;
public class ClaimsDb:DbContext{
 public ClaimsDb(DbContextOptions<ClaimsDb> opt):base(opt){}
 public DbSet<Claim> Claims=>Set<Claim>();
}
