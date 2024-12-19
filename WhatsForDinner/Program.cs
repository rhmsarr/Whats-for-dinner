using Microsoft.EntityFrameworkCore;
using WhatsForDinner.Models;
using WhatsForDinner.Services;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);
//configuring the Identity database
builder.Services.AddDbContext<AppIdentityDbContext>(
    o => o.UseSqlite(
        builder.Configuration["ConnectionStrings:identityDatabase"]
    ));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>();
//Configuring the app so that it registers the cohere settings
builder.Services.Configure<CohereSettings>(builder.Configuration.GetSection("CohereSettings"));
//registering CoherService for http calls
builder.Services.AddHttpClient<CohereService>();
builder.Services.AddControllersWithViews();

//Configuring the application database
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

IdentitySeedData.EnsurePopulated(app);

SeedData.EnsurePopulated(app);//populating the database if needed

app.UseStaticFiles();
app.MapDefaultControllerRoute();




app.Run();


