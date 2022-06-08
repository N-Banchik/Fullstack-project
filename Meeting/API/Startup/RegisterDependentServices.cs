using API.ApplicationStartupExtentions;
using Microsoft.OpenApi.Models;


public static class RegisterDependentServices
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        //Register services/Dependencies here
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
        });
        builder.Services.AddCors();
        //Using Customized services for Dependencies
        builder.Services.AddCustomizedServices(builder.Configuration);
        //Add Identity Platform services
        builder.Services.AddIdentityServices(builder.Configuration);
       
        




        return builder;
    }
}
