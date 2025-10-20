

using Spectre.Console;
using System.Net.Http.Headers;
using System.Net.Http.Json;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");


var request = new HttpRequest(client, "https://api.nbrb.by/exrates/rates?periodicity=0");
var response = await request.SendRequest();

Dictionary<string, ICurrencyStrategy> commandStrategies = new Dictionary<string, ICurrencyStrategy> {
    {"USD", new ConverterUSD(response)},
    {"EUR", new ConverterEUR(response)},
    {"RUB", new ConverterRUB(response)}
};

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
        .PageSize(10)
        .MoreChoicesText("[grey](Нажимай вверх вниз, чтобы выбрать)[/]")
        .AddChoices(
            commandStrategies.Keys
        ));

var converter = commandStrategies[value];

var currency = converter.Convert(price);
// Создание таблицы
var table = new Table();
table.Border = TableBorder.Rounded;

table.AddColumn("BYN");
table.AddColumn(value);

table.AddRow($"{price}", $"[green]{currency}[/]");
AnsiConsole.Write(table);



