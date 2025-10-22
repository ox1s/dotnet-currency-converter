using System.Net.Http.Json;

public class HttpRequest
{
    private readonly HttpClient _client;
    public HttpRequest(HttpClient client)
    {
        _client = client;
    }
    public async Task<List<Currency>> SendRequest()
    {
        var repositories = await _client.GetFromJsonAsync<List<Currency>>(
          "https://api.nbrb.by/exrates/rates?periodicity=0");

        if (repositories is null || repositories.Count == 0)
            throw new InvalidOperationException("Невозможно получить данные о валютах");
    
        return repositories;
    }
}
