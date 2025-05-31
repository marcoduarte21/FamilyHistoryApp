using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using RegistroDeMatriculaDeCentroEducativo.BL.interfaces;
using RegistroDeMatriculaDeCentroEducativo.BL.services;
using RegistroDeMatriculaDeCentroEducativo.Exceptions;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGestorDeLaMatricula, GestorDeLaMatricula>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RegistroDeMatriculaDeCentroEducativo.DA.DBContexto>(x => x.UseSqlServer(connectionString));

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        int statusCode = error switch
        {
            CustomException ex => ex.StatusCode,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.StatusCode = statusCode;

        var response = new { error = error?.Message };
        await context.Response.WriteAsJsonAsync(response);
    });
});

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
