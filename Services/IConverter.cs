namespace DotnetCurrencyConverter.Services;

public interface IConverter
{
    double Convert(double amountBYN, string currencyAbbreviation);
}