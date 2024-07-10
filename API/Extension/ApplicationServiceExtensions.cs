using API.Data;
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
return services;
    }

}
