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
    var userManager = services.GetRequiredService<UserManager<User>>();
    //1. add roleManager 
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
    //await context.Database.MigrateAsync();
    //await roleManager.CreateAsync(new IdentityRole<int>("Member"));
    //await roleManager.CreateAsync(new IdentityRole<int>("Admin"));
    // await context.Categories.AddAsync(new Category { CategoryName = "Category test", Description = "test test" });
   // await context.SaveChangesAsync();


    //await Seed.seedUsers(context);
}
catch (Exception)
{

    throw;
}

app.SetupMiddleware().Run();
