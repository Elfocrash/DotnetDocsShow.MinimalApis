var app = WebApplication.Create();

app.MapGet("helloworld", () => "Hello world!");

app.Run();
