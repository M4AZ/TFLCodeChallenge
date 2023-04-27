namespace TFLCodeChallenge.RestClient.Http;

public abstract class IRestHttpClient {
    
    /**
     * Sets the authorization header using a key
     *
     * @param {string} key The value of the authorization header.
     * @returns {IRestClient}
     */
    public abstract IRestHttpClient withAuthorization(string key);

    /**
     * Adds a segment to the request uri
     */
    public abstract IRestHttpClient withUriSegment(string segment);

    public IRestHttpClient withUriSegment(object segment) {
      if (segment == null) {
        return this;
      }

      return withUriSegment(segment.ToString());
    }

    /**
     * Adds a header to the request.
     *
     * @param key The name of the header.
     * @param value The value of the header.
     */
    public abstract IRestHttpClient withHeader(string key, string value);

    public IRestHttpClient withHeader(string key, object value) {
      if (value == null) {
        return this;
      }
      
      return withHeader(key, value.ToString());
    }

    /**
     * Sets the body of the client request.
     *
     * @param body The object to be written to the request body as form data.
     */
    public abstract IRestHttpClient withFormData(FormUrlEncodedContent body);

    /**
     * Sets the body of the client request.
     *
     * @param body The object to be written to the request body as JSON.
     */
    public abstract IRestHttpClient withJSONBody(object body);

    /**
     * Sets the http method for the request
     */
    public abstract IRestHttpClient withMethod(string method);

    /**
     * Sets the uri of the request
     */
    public abstract IRestHttpClient withUri(string uri);

    /**
     * Adds parameters to the request.
     *
     * @param name The name of the parameter.
     * @param value The value of the parameter, may be a string, object or number.
     */
    public abstract IRestHttpClient withParameter(string name, string value);

    public IRestHttpClient withParameter(string name, bool value) {
      return withParameter(name, value ? "true" : "false");
    }

    public IRestHttpClient withParameter(string name, bool? value) {
        if (!value.HasValue)
        {
            return this;
        }

        return withParameter(name, value.Value);
    }

    public IRestHttpClient withParameter(string name, object value) {
      if (value == null) {
        return this;
      }
      
      return withParameter(name, value.ToString());
    }

    public IRestHttpClient withParameter<T>(string name, IEnumerable<T> value) {
      if (value == null) {
        return this;
      }

      return value.Aggregate(this, (current, val) => current.withParameter(name, val));
    }
    
    /**
     * Run the request and return a promise. This promise will resolve if the request is successful
     * and reject otherwise.
     */
    public ClientResponse<T> go<T>() {
      return sendAsync<T>().Result;
    }
    
    public abstract Task<ClientResponse<T>> sendAsync<T>();
  
}