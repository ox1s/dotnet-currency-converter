public class ConverterRUB : ICurrencyStrategy
{
    private readonly List<Currency> _currencies;
    public ConverterRUB(List<Currency> currencies)
    {
        _currencies = currencies;
    }
    public double Convert(double priceBYN)
    {
        var rub = _currencies.FirstOrDefault(c => c.Cur_Abbreviation == "RUB");

        if (rub?.Cur_OfficialRate is null)
            throw new InvalidOperationException("курс RUB не найден или не действителен");

        return rub.Cur_Scale * priceBYN / rub.Cur_OfficialRate;
    }
}
