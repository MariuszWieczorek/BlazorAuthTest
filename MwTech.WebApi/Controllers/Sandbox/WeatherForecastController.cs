using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace MwTech.WebApi.Controllers.Tyres;


[ApiVersion("1")]
[ApiExplorerSettings(GroupName = "v1")]
public class WeatherForecastController : BaseApiController
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        //_logger.LogInformation("ABC");
        //_logger.LogError(new Exception("ABC"), null);
        // test obsługi błędów
        // throw new NotImplementedException("1234");
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }



    // info https://www.michalbialecki.com/2020/05/09/asp-net-core-5-przekazywanie-parametrow-do-akcji/
    // Przekazywanie parametrów jako część URL
    // Ta metoda zwraca prognozę pogody dla jednego dnia w przyszłości.
    // Parametr daysForward określa, ile dni z wyprzedzeniem należy zwrócić prognozę pogody.
    // Zauważ, że daysForward jest częścią routingu, więc prawidłowy adres URL do tego punktu końcowego
    // będzie wyglądał następująco:

    // GET: weatherForecast/3

    // Możemy również użyć atrybutu [FromRoute] przed deklaracją metody,
    // ale domyślnie będzie również działał w ten sam sposób.
    // public IActionResult Get([FromRoute] int daysForward)

    [Route("{daysForward}")]
    [HttpGet]
    public IActionResult Get1(int daysForward)
    {
        var rng = new Random();
        return new JsonResult(new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(daysForward)),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        });
    }


    // Przekazywanie parametrów w parametrach żądania
    // Jest to bardzo powszechna metoda przekazywania dodatkowych parametrów,
    // ponieważ nie wymaga od nas zmiany routingu, więc jest również kompatybilna wstecz.
    // Kompatybilność jest ważna, w przypadku zmiany istniejącego rozwiązania.
    // Spójrzmy na inną metodę, która zwróciłaby kolekcję prognoz pogody z opcją sortowania

    //W tym przykładzie przekazujemy parametr sortByTemperature,
    //który jest opcjonalny.
    //Zauważ, że używamy atrybutu[FromQuery],
    //aby wskazać, że jest to zmienna pobrana z parametru zapytania.
    //Adres URL tego punktu końcowego wygląda następująco:

    // GET: weatherForecast? sortByTemperature = true

    //Możesz także przekazać więcej parametrów rozdzielając je znakiem &:
    //GET: weatherForecast? key1 = value1 & key2 = value2 & key3 = value3

    // Zwróć uwagę na to, że adres URL musi być odpowiednio zakodowany, aby działał poprawnie.
    // Jeśli chcesz przekazać taki parametr:
    // https://www.michalbialecki.com/?name=Michał Białecki
    // To powinien być zakodowany jako:
    // https://www.michalbialecki.com/?name=Micha%C5%82%20Bia%C5%82ecki



    [HttpGet]
    public IEnumerable<WeatherForecast> Get2([FromQuery] bool sortByTemperature = false)
    {
        var rng = new Random();
        var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        });

        if (sortByTemperature)
        {
            forecasts = forecasts.OrderByDescending(f => f.TemperatureC);
        }

        return forecasts;
    }


    // Przekazywanie obiektu w parametrach żądania
    // Gdy podajesz wiele parametrów zapytania, warto traktować je jako obiekt.Spójrz na poniższy kod:
    // Jak widać, przekazuję klasę WeatherForecastFilters jako parametr zapytania.
    // Zauważ, że używam atrybutu [FromQuery] przed nazwą klasy.


    // GET: weatherForecast/GetFiltered?SortByTemperature=true&City=Poznan

    // Tak więc parametry mogą być przekazywane według ich nazw, nawet jeśli należą do klasy.
    // Co więcej, są one dostępne tylko po ich nazwach, bez żadnego prefiksu klasy,
    // a to oznacza, że wszystkie właściwości muszą mieć unikalne nazwy.

    // Obsługa parametrów zapytania jako właściwości klasy będzie działać,
    // ale tylko wewnątrz tej klasy. Nie będzie działać z zagnieżdżonymi obiektami – ich właściwości nie będą mapowane.
    // Jest to więc raczje zebranie parametrów w jeden obiekt, a nie przekazywanie skomplikowanego zagnieżdżonego obiektu.

    [HttpGet("GetFiltered")]
    public IEnumerable<WeatherForecast> GetFiltered([FromQuery] WeatherForecastFilters filters)
    {
        var rng = new Random();
        var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)],
            City = filters.City
        });

        if (filters.SortByTemperature)
        {
            forecasts = forecasts.OrderByDescending(f => f.TemperatureC);
        }

        return forecasts;
    }


    // Przekazywanie parametrów w nagłówkach
    // Nie wyświetla się w adresie URL, więc jest mniej zauważalne przez użytkownika.
    // Typowym scenariuszem przekazywania parametrów w nagłówku byłoby podanie parametrów autoryzacji
    // lub identyfikatora żądania nadrzędnego, aby umożliwić jego śledzenie.Rzućmy okiem na ten przykład:

    // Aby wysłać żądanie POST, musimy skorzystać z jakiegoś narzędzia.
    // Dobrym pomysłem jest wykorzystanie Postman-a:
    // W zakładce Headers można wprowadzić nagłówki żądania, w tym prypadku parentRequestId.

    [HttpPost]
    public IActionResult Post([FromHeader] string parentRequestId)
    {
        Console.WriteLine($"Got a header with parentRequestId: {parentRequestId}!");
        return new AcceptedResult();
    }

    // Przekazywanie parametrów w treści żądania
    // Najczęstszym sposobem przekazywania danych jest umieszczenie ich w treści żądania.
    // Możemy dodać nagłówek Content-Type z wartością application/json i poinformować odbiorcę,
    // jak interpretować tą treść.Rzućmy okiem na poniższy przykład:
    // Używamy atrybutu [FromBody], aby wskazać,
    // że prognoza zostanie pobrana z treści żądania.
    // W ASP.NET Core w .NET 5 nie musimy deserializować treści żądania,
    // aby przekształcić z typu JSON w obiekt WeatherForecast,
    // będzie to zrobione za nas automatycznie. Aby wysłać żądanie POST, użyjmy ponownie Postman-a:

    // Pamiętaj, że rozmiar treści żądania jest ograniczony przez serwer.
    // Może mieć dowolną maksymalną wartość od 1 MB do 2 GB.
    // W ASP.NET Core 5 domyślny maksymalny rozmiar ciała żądania wynosi około 28 MB,
    // ale można to zmienić. A co jeśli chciałbym wysłać większe pliki,
    // ponad 2 GB? Aby to osiągnąć powinieneś rozważyć wysyłanie treści w postaci strumienia lub wysyłanie jej w częściach.


    [HttpPost]
    public IActionResult Post([FromBody] WeatherForecast forecast)
    {
        Console.WriteLine($"Got a forecast for data: {forecast.Date}!");
        return new AcceptedResult();
    }



}

public class WeatherForecastFilters
{
    public bool SortByTemperature { get; set; }

    public string City { get; set; }
}