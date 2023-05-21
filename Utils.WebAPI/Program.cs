using Utils.Application;
using Utils.Infrastructure.Constants;
using Utils.Infrastructure.Extensions;
using Utils.Persistence.Contexts;
using Utils.Persistence.Extensions;
using Utils.WebAPI.Configurations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");

// Add default services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add custom service to the container
builder.Services.AllowCors(configuration);
builder.Services.AddApplicationServices();
builder.Services.AddPersistence(connectionString, typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name);
builder.Services.AddDateTimeProvider();
builder.Services.AddBackgroundService();


builder.Services.AddMemoryCache(options =>
{
    options.SizeLimit = ConfigurationConstant.DefaultCacheSize;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomResponseWrapper();
app.UseGlobalExceptionHandler();
app.UseAuthorization();
app.MapControllers();
app.Run();
