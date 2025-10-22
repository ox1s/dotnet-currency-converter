using System.Net.Http.Json;

public class HttpRequest
{
    private readonly HttpClient _client;
    private readonly string _url;
    public HttpRequest(HttpClient client, string url)
    {
        _client = client;
        _url = url;
    }
    public async Task<List<Currency>> SendRequest()
    {
        var currencies = await _client.GetFromJsonAsync<List<Currency>>(_url);

        if (repositories is null || repositories.Count == 0)
            throw new InvalidOperationException("Невозможно получить данные о валютах");
    
        return repositories;
    }
}
