using Microsoft.EntityFrameworkCore;
using WhatsForDinner.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DinnerDbContext>(options =>{
    options.UseSqlite(
        builder.Configuration["ConnectionStrings: database"]
    );
});
var app = builder.Build();


app.UseStaticFiles();
app.MapDefaultControllerRoute();

//SeedData.EnsurePopulated(app);


app.Run();


