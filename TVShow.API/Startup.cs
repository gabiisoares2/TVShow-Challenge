using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerUI;
using TVShow.Configuration.Configurations;
using TVShow.Infrastructure.Context;
using TVShow.Service.ExternalServices;
using TVShow.Service.Interfaces.ExternalServices;

namespace TVShow.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }
        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddCors(o => o.AddPolicy("AnyOriginPolicy", builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            }));
            services.AddHttpClient();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.ConfigureDataServices(Configuration);
            services.ConfigureServices(Configuration);
            services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>()
                    .AddEntityFrameworkStores<TVShowDbContext>()
                    .AddDefaultTokenProviders();

            services.AddMvcCore()
                    .AddDataAnnotations()
                    .AddApiExplorer();

            services.AddAuthorization();

            var identity = Configuration.GetSection("IdentityServer");

            services.AddSwaggerGen(c =>
            {
                c.DescribeAllParametersInCamelCase();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TVShow - API",
                    Version = "v1",
                    Description = "Documentation - API TV Show",
                    Contact = new OpenApiContact
                    {
                        Name = "API Tv Show",
                        Email = "gaby_.dc@hotmail.com",
                        Url = new Uri("https://www.linkedin.com/in/gabriela-soares-dev/"),
                    }
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Inside the token JWT with text 'Bearer ' before",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] { }
                            }
                        });
            });

            services.ConfigureAutomapper();

            var myService = services.BuildServiceProvider().GetService<IEpisodeDate>();
            myService.ListAllEpisodes();


        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env,
            IServiceProvider serviceProvider)
        {

            if (env.IsDevelopment())
            {
                app.UseDirectoryBrowser();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseCors("AnyOriginPolicy");

            app.UseRouting();

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API do Backoffice TVShow - ");
                c.RoutePrefix = "";
                c.DocExpansion(DocExpansion.Full);
            });

        }
    }
}
