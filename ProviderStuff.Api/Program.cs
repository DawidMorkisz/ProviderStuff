using Microsoft.EntityFrameworkCore;
using ProviderStuff.Api.BackgroundServices;
using ProviderStuff.Data.Data;
using ProviderStuff.Data.Seed;
using ProviderStuff.Domain.Interfaces.Services;
using ProviderStuff.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProviderStuffDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<IPingService, PingService>();
builder.Services.AddSingleton<IStatusEvaluatorService, StatusEvaluator>();
builder.Services.AddSingleton<IPingDiagnosticService, PingDiagnosticService>();

builder.Services.AddHostedService<PingWorkerBackgroundService>();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProviderStuffDbContext>();
    await dbContext.Database.MigrateAsync();
    await DbSeeder.SeedAsync(dbContext);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();