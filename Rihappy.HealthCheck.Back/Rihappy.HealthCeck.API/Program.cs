using Rihappy.HealthCheck.Application.Interface.Service;
using Rihappy.HealthCheck.Application.Service;
using Rihappy.HealthCheck.Data.Rest.Repositories;
using Rihappy.HealthCheck.Domain.Interface.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IHealthRepository, HealthRepository>(client => {

});

builder.Services.AddScoped<IHealthRepository, HealthRepository>();
builder.Services.AddScoped<IHealthService, HealthService>();

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
