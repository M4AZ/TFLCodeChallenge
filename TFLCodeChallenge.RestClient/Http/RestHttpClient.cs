using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TFLCodeChallenge.RestClient.Http.Domain;

namespace TFLCodeChallenge.RestClient.Http;

public class RestHttpClient :IRestHttpClient
{
    private readonly HttpClient _httpClient;

    private HttpContent _content;

    private string _method = "GET";

    private String _uri = "";

    private List<KeyValuePair<string, string>> _parameters = new List<KeyValuePair<string, string>>();

    private Dictionary<string, string> _headers = new Dictionary<string, string>();

    private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
    {
        NullValueHandling = NullValueHandling.Ignore,
        ContractResolver = new DefaultContractResolver()
    };
    
    public RestHttpClient(HttpClient client)
    {
        _httpClient = client;
    }
    
    /**
     * Sets the authorization header using a key
     *
     * @param {string} key The value of the authorization header.
     * @returns {DefaultRESTClient}
     */
    public override IRestHttpClient withAuthorization(string key)
    {
        withHeader("Authorization", key);
        return this;
    }

    /**
     * Adds a segment to the request uri
     */
    public override IRestHttpClient withUriSegment(string segment)
    {
        if (segment == null) {
            return this;
        }

        if (_uri[_uri.Length - 1] != '/') {
            _uri += '/';
        }

        _uri = _uri + segment;
        return this;
    }

    /**
     * Adds a header to the request.
     *
     * @param key The name of the header.
     * @param value The value of the header.
     */
    public override IRestHttpClient withHeader(string key, string value)
    {
        _headers[key] = value;
        return this;
    }
    
    /**
     * Sets the body of the client request.
     *
     * @param body The object to be written to the request body as form data.
     */
    public override IRestHttpClient withFormData(FormUrlEncodedContent body)
    {
        _content = body;
        return this;
    }

    /**
     * Sets the body of the client request.
     *
     * @param body The object to be written to the request body as JSON.
     */
    public override IRestHttpClient withJSONBody(object body)
    {
        _content = new StringContent(JsonConvert.SerializeObject(body, SerializerSettings), Encoding.UTF8,
            "application/json");
        return this;
    }

    /**
     * Sets the http method for the request
     */
    public override IRestHttpClient withMethod(string method)
    {
        if (method != null) {
            this._method = method;
        }

        return this;
    }

    /**
     * Sets the uri of the request
     */
    public override IRestHttpClient withUri(string uri)
    {
        if (uri != null) {
            this._uri = uri;
        }

        return this;
    }

    /**
     * Adds parameters to the request.
     *
     * @param name The name of the parameter.
     * @param value The value of the parameter, may be a string, object or number.
     */
    public override IRestHttpClient withParameter(string name, string value)
    {
        _parameters.Add(new KeyValuePair<string, string>(name, value));
        return this;
    }
    
    private string getFullUri() {
        if (!_parameters.Any())
        {
            return _uri;
        }

        var encodedParameters = _parameters.Select(p => $"{WebUtility.UrlEncode(p.Key)}={WebUtility.UrlEncode(p.Value)}");

        var queryString = string.Join("&", encodedParameters);
        
        return $"{_uri}?{queryString}";
    }
    
    private Task<HttpResponseMessage> BaseRequest() {
        foreach (var (key, value) in _headers.Select(x => (x.Key, x.Value))) {
            if (key == "Authorization") {
                _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, value);
            } else {
                _httpClient.DefaultRequestHeaders.Add(key, value);
            }
        }

        var requestUri = getFullUri();
        switch (_method.ToUpper()) {
            case "GET":
                return _httpClient.GetAsync(requestUri);
            case "DELETE":
                if (_content != null) {
                    var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
                    request.Content = _content;
                    return _httpClient.SendAsync(request);
                } else {
                    return _httpClient.DeleteAsync(requestUri);
                }
            case "PUT":
                return _httpClient.PutAsync(requestUri, _content);
            case "POST":
                return _httpClient.PostAsync(requestUri, _content);
            case "PATCH":
                var patchRequest = new HttpRequestMessage();
                patchRequest.Method = new HttpMethod("PATCH");
                patchRequest.Content = _content;
                patchRequest.RequestUri = new Uri(requestUri, UriKind.RelativeOrAbsolute);
                return _httpClient.SendAsync(patchRequest);
            default:
                throw new MissingMethodException("This REST client does not support that method.");
        }
    }
    public override Task<ClientResponse<T>> sendAsync<T>() {
        return BaseRequest()
            .ContinueWith(task => {
                var clientResponse = new ClientResponse<T>();
                try
                {
                    var result = task.Result;
                    clientResponse.statusCode = (int)result.StatusCode;
                    if (clientResponse.statusCode >= 300) {
                        clientResponse.errorResponse =
                            JsonConvert.DeserializeObject<Errors>(result.Content.ReadAsStringAsync().Result, SerializerSettings);
                    }
                    else {
                        clientResponse.successResponse =
                            JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result, SerializerSettings);
                    }
                }
                catch (Exception e)
                {
                    clientResponse.exception = e;
                }

                return clientResponse;
            });
    }

}