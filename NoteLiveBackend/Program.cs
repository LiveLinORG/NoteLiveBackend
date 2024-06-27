
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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
using NoteLiveBackend.IAM.Infrastructure.Hashing.BCrypt.Services;
using NoteLiveBackend.Room.Application.Internal.CommandServices;
using NoteLiveBackend.Room.Application.Internal.Queryservices;
using NoteLiveBackend.Room.Domain.Repositories;
using NoteLiveBackend.Room.Domain.Services;
using NoteLiveBackend.Room.Interfaces.REST.Transform;
using NoteLiveBackend.Room.Interfaces.WebSocket;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{

    options.Conventions.Add(new KebabCaseRouteNamingConvention());
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "LiveLin.NotelivePlatform",
                Version = "v1",
                Description = "LiveLin NoteLive Platform API",
                TermsOfService = new Uri("https://notelive.netlify.app"),
                Contact = new OpenApiContact
                {
                    Name = "LiveLinORG",
                    Email = "u202210066@upc.edu.pe"
                },
                License = new OpenApiLicense
                {
                    Name = "Apache 2.0",
                    Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                }
            });
        c.EnableAnnotations();
     
    });

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
// ADD CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsPolicyBuilder =>
    {
        corsPolicyBuilder.WithOrigins("http://localhost:8080", "http://190.239.59.168:44353", "http://190.239.59.168:8080","http://192.168.1.34:8080/professorSession"
                ,"http://190.239.59.168:8080","http://localhost:8080/studentSession")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// ADD REPOSITORIES AND SERVICES
builder.Services.AddSignalR();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IChatRepository,ChatRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IPDFRepository, PDFRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();


builder.Services.AddScoped<IRoomCommandService, RoomCommandService>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IQuestionCommandService, QuestionCommandService>();
builder.Services.AddScoped<IPDFCommandService, PDFCommandService>();
builder.Services.AddScoped<IChatCommandService, ChatCommandService>();



builder.Services.AddScoped<IPDFQueryService, PDFQueryService>();
builder.Services.AddScoped<IUserQueryServices, UserQueryService>();
builder.Services.AddScoped<IQuestionQueryService, QuestionQueryService>();
builder.Services.AddScoped<IRoomQueryService, RoomQueryService>();




builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();

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
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();
app.MapHub<ChatHub>("/chatHub");
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
