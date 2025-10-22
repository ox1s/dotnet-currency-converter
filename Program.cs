

using Spectre.Console;
using System.Net.Http.Headers;
using System.Net.Http.Json;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");


var request = new HttpRequest(client);
var response = await request.SendRequest();
// string[] currencies = { "USD", "EUR", "RUB" };
string[] currencies = response.Select(x => x.Cur_Abbreviation).ToArray();
// Выбор стоимости
var price = AnsiConsole.Prompt(
    new TextPrompt<double>("Сколько перевести(в BYN): ")
      .Validate((n) => n <= 0
      ? ValidationResult.Error("Валюта не божет быть меньше или равна 0")
      : ValidationResult.Success()));

// Выбор валюты
var value = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("Какую валюту использовать при переводе?")
        .PageSize(4)
        .MoreChoicesText("[grey](Нажимай вверх вниз, чтобы выбрать)[/]")
        .AddChoices(
            currencies
        ));




var currencyAbbreviation = value;
IConverter converter = new Converter(response);
var result = converter.Convert(price, currencyAbbreviation);
// Создание таблицы
var table = new Table();
table.Border(TableBorder.Rounded);

table.AddColumn("BYN");
table.AddColumn(value);

table.AddRow($"{price}", $"[indianred1_1]{result}[/]");
AnsiConsole.Write(table);



