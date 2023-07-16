using Microsoft.AspNetCore.StaticFiles;
using MongoFramework;
using PictureStorageService.Datas;
using PictureStorageService.Entitis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


string? DB_CONNECTION_STRING
    = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");
var connection = MongoDbConnection.FromConnectionString(DB_CONNECTION_STRING);

builder.Services.AddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>();
builder.Services.AddSingleton(new AppDbContext(connection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
