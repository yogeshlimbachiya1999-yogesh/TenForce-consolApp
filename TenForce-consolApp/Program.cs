using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TenForce_consolApp.DataAccess;
using TenForce_consolApp.Output;
using TenForce_consolApp.Services;

public class Program
{
    private static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddHttpClient<
            ISolarSystemRepository,
            SolarSystemRepository>();

        builder.Services.AddTransient<
            IPlanetAnalysisService,
            PlanetAnalysisService>();

        builder.Services.AddTransient<ConsoleOutputService>();

        using var host = builder.Build();

        try
        {
            var repository = host.Services.GetRequiredService<
                ISolarSystemRepository>();

            var analysis = host.Services.GetRequiredService<
                IPlanetAnalysisService>();

            var output = host.Services.GetRequiredService<
                ConsoleOutputService>();

            Console.WriteLine("Loading data...");

            var bodies = await repository.GetAllBodiesAsync();

            Console.WriteLine("Processing data...");

            var planets =
                analysis.GetPlanetsWithMoonTemperatures(bodies);

            Console.WriteLine("Writing data...");

            output.DisplayPlanetsWithAverageTemperature(planets);

            Console.WriteLine("Completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}