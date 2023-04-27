namespace TFLCodeChallenge.RestClient.Http.Domain;

public class Errors
{
    public IDictionary<string, List<Error>> fieldErrors;

    public List<Error> generalErrors;

    public Errors with(Action<Errors> action) {
        action(this);
        return this;
    }
}