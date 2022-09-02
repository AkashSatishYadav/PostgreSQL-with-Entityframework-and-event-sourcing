using Microsoft.EntityFrameworkCore;
using PostgresMarten.DataContexts;
using PostgresMarten.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PostgresDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration["ConnectionStrings:DefaultConnection"]);
});
builder.Services.AddMarten(builder.Configuration["ConnectionStrings:DefaultConnection"]);
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "UsersApi_";
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
