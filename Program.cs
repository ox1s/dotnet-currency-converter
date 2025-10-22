
using System.Net.Http.Headers;
using DotnetCurrencyConverter;
using DotnetCurrencyConverter.Models;
using DotnetCurrencyConverter.Services;
using Spectre.Console;




try
{
    using var client = CreateHttpClient();
    var currencyService = new CurrencyService(client);
    var rates = await currencyService.GetExchangeRatesAsync();

    var (amount, targetCurrency) = GetConversionParameters(rates);

    IConverter converter = new Converter(rates);
    var result = converter.Convert(amount, targetCurrency);

    DisplayResult(amount, targetCurrency, result);
}
catch (Exception ex)
{
    AnsiConsole.MarkupLine($"[bold red]Произошла ошибка:[/] {ex.Message}");
    return 1;
}
return 0;


HttpClient CreateHttpClient()
{
    var client = new HttpClient();
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Add("User-Agent", "DotnetCurrencyConverter");
    return client;
}

(double Amount, string Currency) GetConversionParameters(List<Currency> currencies)
{
    var amount = AnsiConsole.Prompt(
        new TextPrompt<double>("Сколько перевести(в BYN): ")
          .Validate((n) => n > 0
            ? ValidationResult.Success()
            : ValidationResult.Error("Сумма не божет быть меньше или равна 0")));


    var currency = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Какую валюту использовать при переводе?")
            .PageSize(4)
            .MoreChoicesText("[grey](Нажимай вверх вниз, чтобы выбрать)[/]")
            .AddChoices(
                currencies.Select(c => c.Cur_Abbreviation)
            ));

    return (amount, currency);
}

void DisplayResult(double amount, string targetCurrency, double result)
{
    var table = new Table()
        .Border(TableBorder.Rounded)
        .AddColumn("BYN")
        .AddColumn(targetCurrency)
        .AddRow($"{amount}", $"[indianred1_1]{result}[/]");

    AnsiConsole.Write(table);
}