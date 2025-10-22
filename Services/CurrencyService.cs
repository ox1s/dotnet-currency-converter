using System.Net.Http.Json;
using DotnetCurrencyConverter.Models;

namespace DotnetCurrencyConverter.Services;

public class CurrencyService
{
    private const string NbrbApiUrl = "https://api.nbrb.by/exrates/rates?periodicity=0";
    private readonly HttpClient _httpClient;

    public CurrencyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Currency>> GetExchangeRatesAsync()
    {
        var currencies = await _httpClient.GetFromJsonAsync<List<Currency>>(NbrbApiUrl);

        if (currencies is null || currencies.Count == 0)
            throw new InvalidOperationException("Невозможно получить данные о валютах");
    
        return currencies;
    }
}