using TFLCodeChallenge.RestClient.Http.Domain;

namespace TFLCodeChallenge.RestClient.Http;

public class ClientResponse<T> {
    
    public int statusCode;

    public T successResponse;

    public Errors errorResponse;

    public Exception exception;

    public bool WasSuccessful() {
        return statusCode >= 200 && statusCode <= 299 && exception == null;
    }
}