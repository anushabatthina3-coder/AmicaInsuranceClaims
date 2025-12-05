using Claims.Application;
using Claims.Infrastructure;
using Microsoft.EntityFrameworkCore;

var b=WebApplication.CreateBuilder(args);
b.Services.AddDbContext<ClaimsDb>(o=>o.UseInMemoryDatabase("AmicaClaims"));
b.Services.AddScoped<IClaimRepository,ClaimRepository>();
b.Services.AddScoped<IClaimService,ClaimService>();
b.Services.AddEndpointsApiExplorer();
b.Services.AddSwaggerGen();

var app=b.Build();
if(app.Environment.IsDevelopment()){app.UseSwagger();app.UseSwaggerUI();}

app.MapPost("/api/claims",async(IClaimService svc,string policy,string customer,decimal amount)=>
 Results.Ok(await svc.SubmitAsync(policy,customer,amount)));

app.MapGet("/api/claims",async(IClaimService svc)=>
 Results.Ok(await svc.GetAllAsync()));

app.MapGet("/api/claims/{id}",async(IClaimService svc,Guid id)=>
 await svc.GetAsync(id) is {} c?Results.Ok(c):Results.NotFound());

app.MapPost("/api/claims/{id}/review",async(IClaimService svc,Guid id,string notes,bool approve)=>
 Results.Ok(await svc.ReviewAsync(id,notes,approve)));

app.Run();
