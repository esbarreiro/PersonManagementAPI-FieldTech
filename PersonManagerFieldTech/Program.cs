using PersonManagerFieldTech.Repository;
using PersonManagerFieldTech.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPersonRepoDependencies();
builder.Services.AddPersonServiceDependencies();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
