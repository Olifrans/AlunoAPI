using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AlunoAPI.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AlunoAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AlunoAPIContext") ?? throw new InvalidOperationException("Connection string 'AlunoAPIContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(option =>
    {
        option.AddPolicy("PolicyOlifrans", olifrans =>
        {
            olifrans.AllowAnyMethod();
            olifrans.AllowAnyHeader();
            olifrans.AllowAnyOrigin();

        });
    }
);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PolicyOlifrans");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
