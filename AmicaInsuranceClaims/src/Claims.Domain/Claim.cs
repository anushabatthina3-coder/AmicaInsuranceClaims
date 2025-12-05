namespace Claims.Domain;
public enum ClaimStatus{Submitted,InReview,Approved,Rejected,Closed}
public class Claim{
 public Guid Id{get;private set;}=Guid.NewGuid();
 public string PolicyNumber{get;private set;}="";
 public string CustomerName{get;private set;}="";
 public decimal ClaimAmount{get;private set;}
 public ClaimStatus Status{get;private set;}=ClaimStatus.Submitted;
 public DateTime SubmittedOn{get;private set;}=DateTime.UtcNow;
 public string? AdjusterNotes{get;private set;}
 private Claim(){} 
 public Claim(string policy,string customer,decimal amount){
  PolicyNumber=policy; CustomerName=customer; ClaimAmount=amount;
 }
 public void AssignNotes(string notes)=>AdjusterNotes=notes;
 public void SetStatus(ClaimStatus st)=>Status=st;
}
