using Microsoft.EntityFrameworkCore;

namespace OMSSample.Models;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    public DbSet<OrderSingle> OrderSingles{ get; set;}
    public DbSet<User>  Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderSingle>()
            .HasOne(os => os.User)
            .WithMany(u => u.NewOrderSingle)
            .HasForeignKey(os => os.UserId);
    }
}