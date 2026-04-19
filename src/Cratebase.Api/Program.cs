using Cratebase.Application;
using Cratebase.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCratebaseApplication();
builder.Services.AddCratebaseInfrastructure();

WebApplication app = builder.Build();

app.MapGet("/health", () =>
{
    HealthResponse response = new()
    {
        Service = "cratebase-api",
        Status = "ok"
    };

    return Results.Ok(response);
})
.WithName("GetHealth");

app.Run();

public partial class Program;

internal sealed class HealthResponse
{
    public required string Service { get; init; }

    public required string Status { get; init; }
}
