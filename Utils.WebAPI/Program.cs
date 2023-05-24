using Utils.WebAPI.Configurations;
using Utils.CrossCuttingConcerns.Constants;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");

// Add default services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add custom service to the container
builder.Services.AllowCors(configuration);
builder.Services.ConfigureDi(configuration);
builder.Services.ConfigureAutoMapper();


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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
