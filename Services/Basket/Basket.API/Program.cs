var builder = WebApplication.CreateBuilder(args);

// Add Services to the container

var app = builder.Build();

app.MapGet("/", () => "Hello World!");


// Configure the http request pipeline

app.Run();
