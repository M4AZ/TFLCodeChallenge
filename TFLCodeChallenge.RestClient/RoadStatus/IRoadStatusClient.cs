using TFLCodeChallenge.RestClient.Http;
using TFLCodeChallenge.RestClient.RoadStatus.Models;

namespace TFLCodeChallenge.RestClient.RoadStatus;


/// <summary>
/// A client for https://restcountries.com/
/// </summary>
/// <remarks>
/// See the <a href="https://restcountries.com/">API documentation</a> for more information.
///</remarks>
public interface IRoadStatusClient
{
    
    /// <summary>
    /// Gets all countries 
    /// This is an asynchronous method.
    /// </summary>
    /// <returns>
    /// When successful, the response will contain the list of all the country names 
    /// </returns>
    Task<ClientResponse<IEnumerable<RoadStatusResponseModel>>> RoadStatus(string code,string appId, string appKey);
    
}