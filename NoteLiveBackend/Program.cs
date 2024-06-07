using Microsoft.EntityFrameworkCore;
using NoteLiveBackend.Shared.Domain.Repositories;
using NoteLiveBackend.Shared.Infraestructure.Interfaces.ASP.Configuration;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Repositories;
using NoteLiveBackend.Users.Aplication.Internal.CommandServices;
using NoteLiveBackend.Users.Aplication.Internal.QueryServices;
using NoteLiveBackend.Users.Domain.Repositories;
using NoteLiveBackend.Users.Domain.Services;
using NoteLiveBackend.Users.Infraestructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new KebabCaseRouteNamingConvention());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ADD DATABASE CONNECTION
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//CONFIGURE DATABASE CONTEXT AND LOGGING LEVELS
builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        if(connectionString!=null)
            if (builder.Environment.IsDevelopment())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            else if (builder.Environment.IsProduction())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Error)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
    });

// CONFIGURE LOWERCASE URLS 
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new KebabCaseRouteNamingConvention());
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAlumnoRepository, AlumnoRepository>();
builder.Services.AddScoped<IAlumnoCommandService, AlumnoCommandService>();
builder.Services.AddScoped<IAlumnoQueryService, AlumnoQueryService>();


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