using System.Runtime.CompilerServices;
using Euris.Examples.Business;
using Euris.Examples.Common.Repositories;
using Euris.Examples.Common.Services;
using Euris.Examples.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IMovieRepository, MovieRepository>();
builder.Services.AddTransient<IMovieService, MovieService>();
builder.Services.AddSingleton<Serilog.ILogger>(logger);
builder.Services.AddSingleton<IDatabaseOptions>(x =>
    new DatabaseOptions(builder.Configuration.GetConnectionString("MoviesDatabase")));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
