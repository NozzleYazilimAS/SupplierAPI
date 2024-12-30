using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SupplierAPI.Configurations
{
    public static class LoggingConfig
    {
        public static void ConfigureLogging(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
                loggingBuilder.AddFile("Logs/supplierapi-{Date}.txt", isJson: true);
            });
        }
    }
}
