using Microsoft.EntityFrameworkCore;
using WhatsForDinner.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DinnerDbContext>(options =>{
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:database"]
    );
});
var app = builder.Build();

//checking for pending migrations at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DinnerDbContext>();
    dbContext.Database.Migrate(); // Apply pending migrations
}

SeedData.EnsurePopulated(app);//populating the database if needed

app.UseStaticFiles();
app.MapDefaultControllerRoute();




app.Run();


