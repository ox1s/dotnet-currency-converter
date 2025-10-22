public class Converter: IConverter
{
    private readonly List<Currency> _currencies;
    public Converter(List<Currency> currencies)
    {
        _currencies = currencies;
    }
    public double Convert(double priceBYN, string currencyAbbreviation)
    {
        var currency = _currencies.FirstOrDefault(c => c.Cur_Abbreviation == currencyAbbreviation);

        if (currency?.Cur_OfficialRate is null)
            throw new InvalidOperationException($"курс {currencyAbbreviation} не найден или не действителен");

        return currency.Cur_Scale *priceBYN / currency.Cur_OfficialRate;
    }
}
