using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Plugins.Http.CSharp;

var httpFactory = HttpClientFactory.Create();

var oldWebApiStep = CreateStep("old_web_api_step", httpFactory, "https://localhost:5001/weatherforecast");
var newWebApiStep = CreateStep("new_web_api_step", httpFactory, "https://localhost:7256/weatherforecast");
var minimalApiStep = CreateStep("minimal_api_step", httpFactory, "https://localhost:1337/weatherforecast");

var oldWebApiScenario = CreateScenario("old_web_api", oldWebApiStep, 10, TimeSpan.FromSeconds(60));
var newWebApiScenario = CreateScenario("new_web_api", newWebApiStep, 10, TimeSpan.FromSeconds(60));
var minimalApiScenario = CreateScenario("minimal_api", minimalApiStep, 10, TimeSpan.FromSeconds(60));

NBomberRunner
    .RegisterScenarios(oldWebApiScenario, newWebApiScenario, minimalApiScenario)
    .Run();

IStep CreateStep(string stepName, IClientFactory<HttpClient> httpClientFactory, string endpoint) =>
    Step.Create(stepName, httpClientFactory, async context =>
    {
        var response = await context.Client.GetAsync(endpoint, context.CancellationToken);

        return response.IsSuccessStatusCode
            ? Response.Ok(statusCode: (int)response.StatusCode)
            : Response.Fail(statusCode: (int)response.StatusCode);
    });

Scenario CreateScenario(string scenarioName, IStep step, int copies, TimeSpan duration) =>
    ScenarioBuilder
        .CreateScenario(scenarioName, step)
        .WithWarmUpDuration(TimeSpan.FromSeconds(5))
        .WithLoadSimulations(Simulation.KeepConstant(copies, duration));
