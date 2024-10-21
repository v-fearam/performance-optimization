using Azure.Identity;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Azure;
using SynchronousIO;
using SynchronousIO.Models;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserProfileService, FakeUserProfileService>();

builder.Services.AddAzureClients(clientBuilder =>
{
   clientBuilder.AddBlobServiceClient(new Uri(builder.Configuration["storage_url"]))
    .WithCredential(new DefaultAzureCredential()); ;
});

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});

var _logger = loggerFactory.CreateLogger<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            _logger.LogError($"Something went wrong: {contextFeature.Error}");

            await context.Response.WriteAsync("Internal Server Error.");
        }
    });
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
