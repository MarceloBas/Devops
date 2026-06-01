using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/health", () => Results.Json(new { status = "ok" }));
app.MapGet("/version", () => Results.Json(new { version = "1.0.0", build = "local" }));

app.Run();
