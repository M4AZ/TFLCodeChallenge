using TFLCodeChallenge.RestClient.Http;
using TFLCodeChallenge.RestClient.RoadStatus.Models;

namespace TFLCodeChallenge.RestClient.RoadStatus;


/// <summary>
/// A client for https://api.tfl.gov.uk/
/// </summary>
/// <remarks>
/// See the <a href="https://api.tfl.gov.uk/">API documentation</a> for more information.
///</remarks>
public interface IRoadStatusClient
{
    
    /// <summary>
    /// Gets road status of a given road 
    /// This is an asynchronous method.
    /// </summary>
    /// <returns>
    /// When successful, the response will contain the status of the road
    /// </returns>
    Task<ClientResponse<IEnumerable<RoadStatusResponseModel>>> RoadStatus(string code,string appId, string appKey);
    
}