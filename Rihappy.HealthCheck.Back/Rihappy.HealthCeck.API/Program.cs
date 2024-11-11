using Rihappy.HealthCheck.Application.Interface.Service;
using Rihappy.HealthCheck.Application.Service;
using Rihappy.HealthCheck.Data.Rest.Repositories;
using Rihappy.HealthCheck.Domain.Interface.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IVtexRepository, VtexRepository>(client =>
{
});

builder.Services.AddScoped<IVtexRepository, VtexRepository>();
builder.Services.AddScoped<IVtexService, VtexService>();
builder.Services.AddScoped<ISuperAppRepository, SuperAppRepository>();
builder.Services.AddScoped<ISuperAppService, SuperAppService>();

var app = builder.Build();
app.UseCors("AllowAllOrigins");

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