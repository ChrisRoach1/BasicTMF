using System.Reflection;
using BasicTMF.Application.Interfaces;
using BasicTMF.Infrastructure.Services;
using DbUp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BasicTMF.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IStudyService, StudyService>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySQL(configuration.GetConnectionString("tmfDB") ?? "");
            });

            return services;
        }
    }
}
