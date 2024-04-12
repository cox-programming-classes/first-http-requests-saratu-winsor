using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CS_First_HTTP_Client;

/// <summary>
/// This service is responsible for handling all interations with
/// the Winsor Apps API. All other services written that need to
/// use Data in Winsor Apps will depend on this ApiService
/// </summary>
public class ApiService
{
    /// <summary>
    /// This is the part of the object that will handle
    /// speaking HTTP and is dedicated to communicating
    /// with this particular Host
    /// </summary>
    private readonly HttpClient _client = new()
    {
        BaseAddress = new("https://forms-dev.winsor.edu")
    };

    /// <summary>
    /// After logging in, store token here so that
    /// it can be used later
    /// </summary>
    private AuthResponse _auth { get; set; }

    /// <summary>
    /// Private Constructor so it can only be initialized in this file
    /// </summary>
    private ApiService() {}

    /// <summary>
    /// Singleton API Service delaration. All services that depend on
    /// this API will reference "ApiService.Current"
    /// </summary>
    public static readonly ApiService Current = new();
    
    /// <summary>
    /// This message must be called successfully before you may use any endpoints
    /// that require authentication
    /// </summary>
    /// <param name="login"> your username and password </param>
    /// <returns> true if and only if Authentication was successful </returns>
    public async Task<bool> AuthenticateAsync(Login login)
    {
        //Start a new request
        HttpRequestMessage request = new(HttpMethod.Post, "api/auth");

        // concert the Login object to JSON test
        string jsonContent = JsonSerializer.Serialize(login);

        //Add the JSON text to the body of the request. you must indicate
        // that the content-type is 'application/json'
        request.Content = new StringContent(jsonContent,
            Encoding.UTF8, "application/json");

        //the await keyword here lets me know that this statement
        //might take a while to process. it allows the rest of the 
        //program to keep going while this is happening.
        var response = await _client.SendAsync(request);

        //Get the text from the response body. There is some assumption here...
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            //this means that something went wrong, probably a failed login
            //print the response to the Debug Output tab
            Debug.WriteLine(responseContent);
            //login was not successful
            return false;
        }

        _auth = JsonSerializer.Deserialize<AuthResponse>(responseContent);
        return true;
    }
    /// <summary>
    /// Generic API call to the Winsor Apps API that does not have Body Content
    /// </summary>
    /// <param name="method"> GET, POST, PUT, etc </param>
    /// <param name="endpoint"> Endpoint and QueryString if applicable </param>
    /// <param name="authorize"> Does this require authorization? </param>
    /// <typeparam name="TOut"> Type of the expected response to this API call</typeparam>
    /// <returns> the content of the API response </returns>
    public async Task<TOut?> SendAsync<TOut>(HttpMethod method, string endpoint, bool authorize = true)
    {
        HttpRequestMessage request = new(method, endpoint);

        if (authorize)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _auth.jwt);
        }

        var response = await _client.SendAsync(request);
        // Get the text from the response body. There is some assumption here...
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return JsonSerializer
                .Deserialize<TOut>(responseContent);

        //This means that something went wrong...
        Debug.WriteLine(responseContent);
        // the call failed, so return a non-answer
        return default;
    }
/// <summary>
/// 
/// </summary>
/// <param name="method"> GET, POST, PUT, etc </param>
/// <param name="endpoint"> Endpoint and QueryString if applicable </param>
/// <param name="content"> The content of this API call </param>
/// <param name="authorize"> Does this require authorization? </param>
/// <typeparam name="TIn"> Type of the content being sent </typeparam>
/// <returns></returns>
    public async Task<bool> SendAsync<TIn>(HttpMethod method, string endpoint, TIn content, bool authorize = true)
    {
        HttpRequestMessage request = new(method, endpoint);

        if (authorize)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _auth.jwt);
        }

        var jsonContent = JsonSerializer.Serialize(content);

        var response = await _client.SendAsync(request);
        // Get the text from the response body. There is some assumption here...
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return true;

        // This means that something went wrong...
        Debug.WriteLine(responseContent);
        // the call failed, so return a non-answer
        return false;
        
        
    }

}

