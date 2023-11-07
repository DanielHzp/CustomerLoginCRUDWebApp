using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserLoginCRUDWebApp.Models;

namespace UserLoginCRUDWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<UserLoginCRUDWebApp.Models.UserComment>? UserComment { get; set; }
    }
}