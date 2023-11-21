using ChargeMe.API.DataBase;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using telemetry_api.Import.Service;

[assembly: FunctionsStartup(typeof(telemetry_api.Startup))]
namespace telemetry_api
{
    public class Startup: FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddLogging();

            var executionContextOptions = builder.Services.BuildServiceProvider()
                .GetService<IOptions<ExecutionContextOptions>>().Value;
            var currentDirectory = executionContextOptions.AppDirectory;


            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            builder.Services.AddSingleton(config);

            builder.Services.AddDbContext<DBContext>(options =>
               options.UseSqlServer(config.GetConnectionString("SqlServerConnectionString"),
               options => options.EnableRetryOnFailure()));
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<ITelemetryRepo, TelemetryRepo>();
            builder.Services.AddScoped<IImportService, ImportService>();
        }
       
    }
}
