using TFLCodeChallenge.RestClient.Http;
using TFLCodeChallenge.RestClient.RoadStatus.Models;

namespace TFLCodeChallenge.RestClient.RoadStatus;

public class RoadStatusClient : IRoadStatusClient
{
    private readonly HttpClient _client;
    
    public RoadStatusClient(HttpClient client)
    {
        _client = client;
    }
    public async Task<ClientResponse<IEnumerable<RoadStatusResponseModel>>> RoadStatus(string code,string appId, string appKey)
    {
        return await Client()
                .withUri("Road")
                .withUriSegment(code.ToUpper())
                .withParameter("app_id",appId)
                .withParameter("app_key",appKey)
                .withMethod("Get")
                .sendAsync<IEnumerable<RoadStatusResponseModel>>();
    }
    
    RestHttpClient Client()
    { 
        return new RestHttpClient(_client);
    }
}