using TFLCodeChallenge;
using TFLCodeChallenge.RestClient.RoadStatus;
using TFLCodeChallengeTests.Mock;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace TFLCodeChallengeTests;

public class RoadStatusTests
{
    // Basic test case to demonstrate the controller and RestClient are unit testing friendly.
     
    private readonly RoadStatus _roadStatus;

    public RoadStatusTests()
    {
        IRoadStatusClient client = new RoadStatusClientMock();
        IConfiguration configuration = new ConfigurationManager();

        _roadStatus = new RoadStatus(client,configuration);
    }
    
    [Theory]
    [InlineData("a2")]
    public async Task DisplayRoadNameOnValidRoad(string code)
    {
        // Act
        await _roadStatus.Run(code);
        
        // Assert
        _roadStatus.DisplayName.Should().Be("A2");
    }
    [Theory]
    [InlineData("a2")]
    public async Task DisplayStatusSeverityOnValidRoad(string code)
    {
        // Act
        await _roadStatus.Run(code);
        
        // Assert
        _roadStatus.StatusSeverity.Should().Be("Good");
    }
    
    [Theory]
    [InlineData("a2")]
    public async Task DisplayStatusSeverityDescriptionOnValidRoad(string code)
    {
        // Act
        await _roadStatus.Run(code);
        
        // Assert
        _roadStatus.StatusSeverityDescription.Should().Be("No Exceptional Delays");
    }
    
    [Theory]
    [InlineData("a233")]
    public async Task ReturnNonZeroSystemErrorCode(string code)
    {
        // Act
        var result= await _roadStatus.Run(code);
        
        // Assert
        result.Should().Be(1);
    }
    
}