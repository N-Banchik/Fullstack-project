using API.Data;
using Microsoft.EntityFrameworkCore;

public static class AddAppServices
{
    public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration _config)
    {
        services.AddDbContext<DataContext>(x => x.UseSqlServer(_config.GetConnectionString("DefaultConnection")));
        return services;
    }
}

