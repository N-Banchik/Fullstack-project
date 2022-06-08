using Classlibrery.Data;
using Classlibrery.DataAccessLayer.IRepository;
using Classlibrery.DataAccessLayer.Reposetories;
using Classlibrery.Services;
using Classlibrery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

public static class AddAppServices
{
    public static IServiceCollection AddCustomizedServices(this IServiceCollection services,IConfiguration _config)
    {
        services.AddDbContext<DataContext>(x => x.UseSqlServer(_config.GetConnectionString("DefaultConnection")));
        //Add Unit of work to the service collection
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        //Add Token Configuration service
        services.AddScoped<ITokenService, TokenService>();
        


        return services;
    }
}

