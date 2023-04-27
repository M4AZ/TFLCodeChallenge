namespace TFLCodeChallenge.RestClient.Http.Domain;

public class Error
{
    public string code;

    public IDictionary<string, object> data;

    public string message;

    public Error with(Action<Error> action) {
        action(this);
        return this;
    }
}