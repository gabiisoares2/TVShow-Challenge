using Serilog;

namespace TVShow.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("messages.json", optional: true, reloadOnChange: true);
                });
        })
        .UseSerilog()
        .ConfigureServices(services =>
        {
            //services.AddHostedService<Seed>();
        });
    }
}