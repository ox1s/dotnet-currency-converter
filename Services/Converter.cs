using DotnetCurrencyConverter.Models;

namespace DotnetCurrencyConverter.Services;

public class Converter : IConverter
{
    private readonly List<Currency> _currencies;

    public Converter(List<Currency> currencies)
    {
        _currencies = currencies;
    }

    public double Convert(double amountBYN, string currencyAbbreviation)
    {
        var currency = _currencies.FirstOrDefault(c => c.Cur_Abbreviation == currencyAbbreviation);

        if (currency?.Cur_OfficialRate is null)
            throw new InvalidOperationException($"курс {currencyAbbreviation} не найден или не действителен");

        return amountBYN / currency.Cur_OfficialRate * currency.Cur_Scale;
    }
}