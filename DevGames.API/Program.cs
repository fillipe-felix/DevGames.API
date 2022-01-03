using System.Reflection;

using DevGames.API.Mappers;
using DevGames.API.Persistence;
using DevGames.API.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(BoardMapper));

var connectionString = builder.Configuration.GetConnectionString("DevGamesCs");

builder.Services.AddDbContext<DevGamesContext>(o => o.UseSqlServer(connectionString));
builder.Services.AddScoped<IBoardRepository, BoardRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DevGames API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Fillipe",
            Email = "felipesoares_1993@hotmail.com"
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    s.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

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
