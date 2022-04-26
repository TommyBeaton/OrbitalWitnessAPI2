using Microsoft.EntityFrameworkCore;
using OrbitalWitnessAPI.API;
using OrbitalWitnessAPI.Context;
using OrbitalWitnessAPI.Factories;
using OrbitalWitnessAPI.Interfaces;
using OrbitalWitnessAPI.Repositories;
using OrbitalWitnessAPI.Utils.ScheduleDataParser;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<OrbitalWitnessContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IParsedDataRepository, ParsedDataRepository>();
builder.Services.AddTransient<IParsedScheduleDtoFactory, ParsedScheduleDtoFactory>();
builder.Services.AddTransient<IParsedScheduleOrmFactory, ParsedScheduleOrmFactory>();
builder.Services.AddTransient<IOWLegacyApiWrapper, OWLegacyApiWrapper>();
builder.Services.AddTransient<IScheduleParser, ScheduleDataParser>();
builder.Services.AddTransient<IScheduleSegmentFactory, ScheduleSegmentFactory>();
builder.Services.AddScoped<IOrbitalWitnessContext, OrbitalWitnessContext>();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
