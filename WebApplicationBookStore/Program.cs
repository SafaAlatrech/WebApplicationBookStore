using WebApplicationBookStore.Models;
using WebApplicationBookStore.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddScoped<IBookStoreRepository<Author>, AuthorDbRepository>();
builder.Services.AddScoped<IBookStoreRepository<Book>, BookDbRepositorycs>();
builder.Services.AddControllers();
//builder.Services.AddDbContext<BookStoreDbContext>(options =>
//{
//    //options.UseSqlServer(IConfiguration.GetConnecionString("SqlConnexion"));
//});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();


}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run(); 

