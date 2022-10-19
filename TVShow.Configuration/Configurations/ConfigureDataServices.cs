using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TVShow.Infrastructure.Audit;
using TVShow.Infrastructure.Context;

namespace TVShow.Configuration.Configurations
{
    public static class ServiceCollections
    {
        public static void ConfigureDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
               .AddEntityFrameworkSqlServer()
               .AddDbContext<TVShowDbContext>((serviceProvider, opts) => opts
               .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning))
               .UseSqlServer(configuration.GetConnectionString("DefaultConnection"), builder =>
               {
                    builder.EnableRetryOnFailure(3, TimeSpan.FromSeconds(2), null);
                    builder.CommandTimeout(300);
                   
               }));

            services.AddScoped<AuditEntrySaveChangesHandler<TVShowDbContext>>();

        }
    }
}
