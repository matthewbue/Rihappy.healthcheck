using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rihappy.HealthCheck.Application.Interface.Service;
using Rihappy.HealthCheck.Application.Service;
using Rihappy.HealthCheck.Data.Rest.Repositories;
using Rihappy.HealthCheck.Data.Rest.Settings;
using Rihappy.HealthCheck.Domain.Interface.Repositories;
using System.Text;

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

var secretKey = Encoding.UTF8.GetBytes("my_super_secure_and_longer_secret_key_123!"); // Substitua por uma chave mais segura
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "AuthAPI", //Emissor
            ValidAudience = "AuthAPI", //Público
            IssuerSigningKey = new SymmetricSecurityKey(secretKey)
        };
    });

builder.Services.Configure<HealthCheckSettings>(builder.Configuration.GetSection("HealthCheckSettings"));

builder.Services.AddScoped<IVtexRepository, VtexRepository>();
builder.Services.AddScoped<IVtexService, VtexService>();
builder.Services.AddScoped<ISuperAppRepository, SuperAppRepository>();
builder.Services.AddScoped<ISuperAppService, SuperAppService>();

builder.Services.AddSingleton<AuthService>();

var app = builder.Build();
app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();