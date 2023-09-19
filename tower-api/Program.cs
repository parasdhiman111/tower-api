using AutoMapper;
using credit_work_app.Data;
using credit_work_app.Utilities;
using Microsoft.EntityFrameworkCore;
using tower_api.Business.Implementations;
using tower_api.Business.Interfaces;
using tower_api.Repositories.Implementations;
using tower_api.Repositories.Interfaces;
using tower_api.Repositories.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICardsGetUseCase, CardsGetUseCase>();
builder.Services.AddScoped<ICardsCreateUseCase, CardsCreateUseCase>();
builder.Services.AddScoped<ICardRepository, CardRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:3000") // Replace with your Angular app's URL
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials(); // If you need to send credentials (e.g., cookies)
    });
});

// Configure services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Manually configure AutoMapper
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<AutoMapperProfile>(); // Use your AutoMapper profile(s)
});

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    // Ensure the database is created and apply migrations
    context.Database.EnsureCreated();

    // Check if there are no categories, and if so, seed them
    if (!context.Cards.Any())
    {
        context.Cards.AddRange(
                new Card { Name = "User1", CreditCard = "1234567890123456", CVC = "123", ExpiryDate = "08/24" },
                new Card { Name = "User2", CreditCard = "1234567890123456", CVC = "456", ExpiryDate = "12/29" }
        );

        context.SaveChanges();
    }
}



app.Run();

