public class ConverterEUR: ICurrencyStrategy
{
    private readonly List<Currency> _currencies;
    public ConverterEUR(List<Currency> currencies)
    {
        _currencies = currencies;
    }
    public double Convert(double priceBYN)
    {
        var eur = _currencies.FirstOrDefault(c => c.Cur_Abbreviation == "EUR");

        if (eur?.Cur_OfficialRate is null)
            throw new InvalidOperationException("курс EUR не найден или не действителен");

        return eur.Cur_Scale *priceBYN / eur.Cur_OfficialRate;
    }
}
