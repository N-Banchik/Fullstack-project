using API.Startup;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

WebApplication app = WebApplication.CreateBuilder(args)
    .RegisterServices()
    .Build();

using IServiceScope scope = app.Services.CreateScope();
IServiceProvider services = scope.ServiceProvider;

try
{
    DataContext context = services.GetRequiredService<DataContext>();
    UserManager<User>? userManager = services.GetRequiredService<UserManager<User>>();
    //1. add roleManager 
    RoleManager<IdentityRole<int>>? roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();


    SeedData seed = new SeedData(context, userManager, roleManager);
   await seed.Seed();
}
catch (Exception)
{

    throw;
}

app.SetupMiddleware().Run();
