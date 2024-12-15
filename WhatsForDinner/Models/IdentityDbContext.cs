using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WhatsForDinner.Models{
    public class AppIdentityDbContext : IdentityDbContext<IdentityUser>{
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        : base(options){

        }
    }
}