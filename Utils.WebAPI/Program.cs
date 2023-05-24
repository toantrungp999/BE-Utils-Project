using Utils.WebAPI.Configurations;
using Utils.CrossCuttingConcerns.Constants;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Utils.Application.Configurations;
using Utils.CrossCuttingConcerns.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add default services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on text box below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

// Add custom service to the container
builder.Services.ConfigureFluentValidation();
builder.Services.AllowCors(configuration);
builder.Services.ConfigureDi(configuration);
builder.Services.ConfigureAutoMapper();

builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings"));


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
