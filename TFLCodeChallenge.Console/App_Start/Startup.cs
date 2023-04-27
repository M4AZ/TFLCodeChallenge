using Microsoft.Extensions.Configuration;
using TFLCodeChallenge.RestClient.RoadStatus;
using Microsoft.Extensions.DependencyInjection;

namespace TFLCodeChallenge;
public class Startup
{
    public static ServiceProvider Init()
    {
        //setup configuration file 
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

        var configuration = builder.Build();
        
        //setup our DI
        var services = new ServiceCollection();

        services.AddHttpClient<IRoadStatusClient, RoadStatusClient>(c =>
        {
            var baseUrl = configuration.GetSection("ApiSettings:BaseUrl").Value;
            
            if (baseUrl != null)
            {
                c.BaseAddress = new Uri(baseUrl);
            }
        });
        services.AddSingleton<IConfiguration>(configuration);
        services.AddSingleton<IRoadStatus, RoadStatus>();

        return services.BuildServiceProvider();
        
    }
}