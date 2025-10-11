using System;
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();

        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

        });

        services.AddCors();

        return services;
    }
}
