using System.Net.Http.Json;

public class HttpRequest
{
    private readonly HttpClient _client;
    public HttpRequest(HttpClient client)
    {
        _client = client;
    }
    public async Task<List<Currency>> SendRequest(string _url)
    {
        var repositories = await _client.GetFromJsonAsync<List<Currency>>(_url);

        if (repositories is null || repositories.Count == 0)
            throw new InvalidOperationException("Невозможно получить данные о валютах");
    
        return repositories;
    }
}
