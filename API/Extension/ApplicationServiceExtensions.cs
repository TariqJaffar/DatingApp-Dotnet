using API.Data;
using API.Helpers;
using API.Interface;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extension;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(Options =>
        {
            Options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        });
        services.AddControllers();

        services.AddCors();

        services.AddScoped<ITokenService, TokenServices>();
        services.AddScoped<IUserRepository,UserRepository>();
       services.AddScoped<IPhotoService,PhotoService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        return services;
    }

}
