using DataAccess.Data;
using DataAccess.DataAccessLayer.IRepository;
using DataAccess.DataAccessLayer.Reposetories;
using DataAccess.Services;
using DataAccess.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

public static class AddAppServices
{
    public static IServiceCollection AddCustomizedServices(this IServiceCollection services,IConfiguration _config)
    {
        services.AddDbContext<DataContext>(x =>
        x.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("DataAccess")));
        //Add Unit of work to the service collection
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        //Add Token Configuration service
        services.AddScoped<ITokenService, TokenService>();
        


        return services;
    }
}

