using TFLCodeChallenge.RestClient.Http;
using TFLCodeChallenge.RestClient.RoadStatus;
using TFLCodeChallenge.RestClient.RoadStatus.Models;

namespace TFLCodeChallengeTests.Mock;

public class RoadStatusClientMock : IRoadStatusClient
{
    private readonly List<RoadStatusResponseModel> _roadStatusResponse;
    
    public RoadStatusClientMock()
    {
        _roadStatusResponse = new List<RoadStatusResponseModel>()
        {
            new()
            {
                type = "Tfl.Api.Presentation.Entities.RoadCorridor, Tfl.Api.Presentation.Entities",
                id="a2",
                displayName = "A2",
                statusSeverity ="Good",
                statusSeverityDescription ="No Exceptional Delays",
                bounds ="[[-0.0857,51.44091],[0.17118,51.49438]]",
                envelope ="[[-0.0857,51.44091],[-0.0857,51.49438],[0.17118,51.49438],[0.17118,51.44091],[-0.0857,51.44091]]",
                url ="/Road/a2"
            }
        };
    }

    public Task<ClientResponse<IEnumerable<RoadStatusResponseModel>>> RoadStatus(string code,string appId, string appKey)
    {
        var response = _roadStatusResponse.Where(x => x.id.ToLower() == code).ToList();
        
        ClientResponse<IEnumerable<RoadStatusResponseModel>> mockResponse;
        
        if (response.Count>0)
        {
            mockResponse =
                new ClientResponse<IEnumerable<RoadStatusResponseModel>>
                {
                    successResponse = response,
                    statusCode = 200
                };
           
        }
        else
        {
            mockResponse =
                new ClientResponse<IEnumerable<RoadStatusResponseModel>>
                {
                    successResponse = response,
                    statusCode = 400
                };
            
        }
        return Task.FromResult(mockResponse);

      
    }
}