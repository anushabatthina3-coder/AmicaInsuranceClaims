using Claims.Domain;
namespace Claims.Application;
public class ClaimService:IClaimService{
 private readonly IClaimRepository _repo;
 public ClaimService(IClaimRepository repo)=>_repo=repo;
 public async Task<ClaimDto> SubmitAsync(string policy,string customer,decimal amount){
  var c=new Claim(policy,customer,amount);
  await _repo.AddAsync(c); await _repo.SaveAsync();
  return Map(c);
 }
 public Task<ClaimDto?> GetAsync(Guid id)=>_repo.GetAsync(id).ContinueWith(t=>t.Result is null?null:Map(t.Result));
 public async Task<List<ClaimDto>> GetAllAsync()=> (await _repo.GetAllAsync()).Select(Map).ToList();
 public async Task<ClaimDto> ReviewAsync(Guid id,string notes,bool approve){
  var c=await _repo.GetAsync(id)??throw new Exception("Not found");
  c.AssignNotes(notes);
  c.SetStatus(approve?ClaimStatus.Approved:ClaimStatus.Rejected);
  await _repo.SaveAsync();
  return Map(c);
 }
 private static ClaimDto Map(Claim c)=>new(c.Id,c.PolicyNumber,c.CustomerName,c.ClaimAmount,c.Status.ToString(),c.AdjusterNotes);
}
