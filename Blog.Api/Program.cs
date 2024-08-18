using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Blog.BL.Managers.Abstract;
using Blog.BL.Managers.Concrete;
using Blog.Entities.DbContexts;
using Blog.Entities.Models.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add DbContext with MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21))));

// Add Dependency Injection for Managers and TokenService
builder.Services.AddScoped<IManager<Category>, CategoryManager>();
builder.Services.AddScoped<IManager<User>, UserManager>();
builder.Services.AddScoped<IManager<Role>, RoleManager>();
builder.Services.AddScoped<IManager<Post>, PostManager>();
builder.Services.AddScoped<IManager<Comment>, CommentManager>();
builder.Services.AddScoped<IManager<PostLike>, PostLikeManager>();
builder.Services.AddScoped<IManager<CommentLike>, CommentLikeManager>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Global error handling middleware
app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke();
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
        // Log the exception (optional)
    }
});

app.Run();