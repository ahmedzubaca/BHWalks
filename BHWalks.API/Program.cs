using BHWalks.API.Data;
using BHWalks.API.Repositories;
using BHWalks.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BHWalksDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BHWalksConnStr"));
});
builder.Services.AddScoped<IRegionsRepository, RegionsRepository>();
builder.Services.AddScoped<IWalksRepository, WalksRepository>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddControllersWithViews()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

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
