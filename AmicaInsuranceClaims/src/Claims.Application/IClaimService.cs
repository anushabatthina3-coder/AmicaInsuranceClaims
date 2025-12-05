using Claims.Domain;
namespace Claims.Application;
public interface IClaimService{
 Task<ClaimDto> SubmitAsync(string policy,string customer,decimal amount);
 Task<ClaimDto?> GetAsync(Guid id);
 Task<List<ClaimDto>> GetAllAsync();
 Task<ClaimDto> ReviewAsync(Guid id,string notes,bool approve);
}
