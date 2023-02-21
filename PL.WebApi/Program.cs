using BLL.Services;
using BLL.Services.Contracts;
using BLL.Validators;
using DAL.Context;
using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PL.WebApi.Extensions;
using PL.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibraryDbContext>
(o => o.UseInMemoryDatabase("LibraryDb"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.AllowAnyOrigin().
        AllowAnyHeader().
        AllowAnyMethod();
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddValidatorsFromAssemblyContaining<AddBookValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ReviewDtoValidator>();

builder.Services.AddScoped<IRepository<Book>, Repository<Book>>();
builder.Services.AddScoped<IRepository<Review>, Repository<Review>>();
builder.Services.AddScoped<IRepository<Rating>, Repository<Rating>>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IRatingService, RatingService>();

var app = builder.Build();
app.UseHttpLogging();
app.SeedData();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseCors("EnableCORS");

app.UseAuthorization();

app.MapControllers();

app.Run();
