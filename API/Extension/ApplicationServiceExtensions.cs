﻿using API.Data;
using API.Interface;
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
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }

}
