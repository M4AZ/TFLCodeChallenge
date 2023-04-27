using Microsoft.Extensions.Configuration;
using TFLCodeChallenge.RestClient.RoadStatus;
using TFLCodeChallenge.RestClient.RoadStatus.Models;

namespace TFLCodeChallenge;
using TFLCodeChallenge.RestClient;

public class RoadStatus : IRoadStatus
{
    private readonly IRoadStatusClient _client;
    private readonly IConfiguration _configuration;
    
    public string? DisplayName;
    public string? StatusSeverity;
    public string? StatusSeverityDescription;
    public RoadStatus(IRoadStatusClient client, IConfiguration configuration)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));;
        _configuration = configuration;
    }
    
    public async Task<int> Run(string? input)
    {
        if (!String.IsNullOrEmpty(input) && !String.IsNullOrWhiteSpace(input))
        {
            var response = await _client.RoadStatus(input,_configuration.GetSection("ApiSettings:AppId").Value,_configuration.GetSection("ApiSettings:AppKey").Value);

            if (response.WasSuccessful())
            {
                var roadStatusResponseModel = response.successResponse.SingleOrDefault();
                
                DisplayName = roadStatusResponseModel?.displayName;
                StatusSeverity = roadStatusResponseModel?.statusSeverity;
                StatusSeverityDescription = roadStatusResponseModel?.statusSeverityDescription;
                
                Console.WriteLine($"The status of the {DisplayName} is as follows");
                Console.WriteLine($"Road Status is {StatusSeverity}");
                Console.WriteLine($"Road Status Description is {StatusSeverityDescription}");
                
                return 0;
            }

            Console.WriteLine($"{input} is not a valid road");
            return 1;
        }
        
        Console.WriteLine($"Please enter a valid input");
        return -1;
    }
}