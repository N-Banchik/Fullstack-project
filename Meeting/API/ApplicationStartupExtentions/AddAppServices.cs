using DataAccess.Data;
using DataAccess.Repository.IRepository;
using DataAccess.Repository.Reposetories;
using DataAccess.Services;
using DataAccess.Services.Interfaces;
using DataAccess.Services.Photos_Services;
using Microsoft.EntityFrameworkCore;


public static class AddAppServices
{
    public static IServiceCollection AddCustomizedServices(this IServiceCollection services, IConfiguration _config)
    {
        services.AddDbContext<DataContext>(x =>
        x.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("DataAccess")));
        //Add Unit of work to the service collection
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        //Add Token Configuration service
        services.AddScoped<ITokenService, TokenService>();
        //Add Photo service
        services.AddScoped<IPhotoService, PhotoService>();
        //Add Mapper service
        services.AddAutoMapper(typeof(MapperService).Assembly);
        //Add Cloudinary service
        services.Configure<CloudinarySettings>(_config.GetSection("CloudinarySettings"));




        return services;
    }
}

