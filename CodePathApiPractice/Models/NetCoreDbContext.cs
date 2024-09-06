using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePathWebAPI.Models;
public class NetCoreDbContext(DbContextOptions<NetCoreDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Post> Posts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Post>().ToTable("Post");
    }
}
