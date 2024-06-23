
using Microsoft.EntityFrameworkCore;
using NoteLiveBackend.IAM.Application.Internal.CommandServices;
using NoteLiveBackend.IAM.Application.Internal.QueryServices;
using NoteLiveBackend.IAM.Domain.Repositories;
using NoteLiveBackend.IAM.Domain.Services;
using NoteLiveBackend.IAM.Infrastructure.Persistence.EFC.Repositories;
using NoteLiveBackend.Room.Infraestructure.Repositories;
using NoteLiveBackend.Shared.Domain.Repositories;
using NoteLiveBackend.Shared.Infraestructure.Interfaces.ASP.Configuration;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Repositories;
using NoteLiveBackend.IAM.Infrastructure.Tokens.JWT.Services; 
using NoteLiveBackend.IAM.Application.Internal.OutboundServices;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new KebabCaseRouteNamingConvention());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ADD DATABASE CONNECTION
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// CONFIGURE DATABASE CONTEXT AND LOGGING LEVELS
builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        if(connectionString != null)
        {
            if (builder.Environment.IsDevelopment())
            {
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            }
            else if (builder.Environment.IsProduction())
            {
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Error)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            }
        }
    });

// CONFIGURE LOWERCASE URLS 
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// ADD CONTROLLERS
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new KebabCaseRouteNamingConvention());
});

// ADD REPOSITORIES AND SERVICES
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ChatRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryServices, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();



// Register additional repositories and services here

var app = builder.Build();

// VERIFY DATABASE OBJECTS ARE CREATED
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

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
