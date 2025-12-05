namespace Claims.Domain;
public class Policy{
 public Guid Id{get;private set;}=Guid.NewGuid();
 public string PolicyNumber{get;private set;}="";
 public string HolderName{get;private set;}="";
 public decimal CoverageLimit{get;private set;}
 private Policy(){}
 public Policy(string number,string holder,decimal limit){
  PolicyNumber=number; HolderName=holder; CoverageLimit=limit;
 }
}
