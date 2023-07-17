using Microsoft.AspNetCore.Server.Kestrel.Core;
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

builder.Services.Configure<KestrelServerOptions>(opt =>
{
    opt.Limits.MaxRequestBodySize = 20 * 1_000_000;
});
builder.Services.Configure<IISServerOptions>(opt =>
{
    opt.MaxRequestBodyBufferSize = 20 * 1_000_000;
});

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
