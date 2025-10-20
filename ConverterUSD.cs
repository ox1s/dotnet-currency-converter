
using System.Net.Http.Json;

public class ConverterUSD : ICurrencyStrategy
{
    private readonly List<Currency> _currencies;
    public ConverterUSD(List<Currency> currencies)
    {
        _currencies = currencies;
    }
    public  double Convert(double priceBYN)
    {
        var usd = _currencies.FirstOrDefault(c => c.Cur_Abbreviation == "USD");

        if (usd?.Cur_OfficialRate is null)
            throw new InvalidOperationException("курс USD не найден или не действителен");

        return usd.Cur_Scale * priceBYN / usd.Cur_OfficialRate;
    }

}
