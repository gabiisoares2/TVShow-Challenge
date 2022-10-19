using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TVShow.Infrastructure.Repository;
using TVShow.Service.ExternalServices;
using TVShow.Service.Interfaces.ExternalServices;
using TVShow.Service.Interfaces.Repository;
using TVShow.Service.Interfaces.Service;
using TVShow.Service.Services;

namespace TVShow.Configuration.Configurations
{
    public static class ConfigureServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IEpisodeDate, EpisodeDate>();
            services.AddScoped<ITvShowRespository, TvShowRepository>();
            services.AddScoped<ICatalogService, CatalogService>();
        }
    }
}
