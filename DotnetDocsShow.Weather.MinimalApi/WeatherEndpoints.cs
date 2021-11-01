namespace DotnetDocsShow.Weather.MinimalApi;

public static class WeatherEndpoints
{
    private static Random Random = new(489);
    private static string[] Summaries = {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public static void MapWeatherEndpoints(this WebApplication app)
    {
        app.MapGet("weatherforecast", () =>
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Next(-20, 55),
                    Summary = Summaries[Random.Next(Summaries.Length)]
                })
                .ToArray();
        })
            .Produces(200, typeof(IEnumerable<WeatherForecast>))
            .AllowAnonymous();
    }
}
