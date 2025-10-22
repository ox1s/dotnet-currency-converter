namespace DotnetCurrencyConverter.Models;

public record class Currency(
    string Cur_Name,
    string Cur_Abbreviation,
    double Cur_OfficialRate,
    int Cur_Scale,
    DateTime Date
)
{
    public DateTime LastDate => Date.ToLocalTime();
}