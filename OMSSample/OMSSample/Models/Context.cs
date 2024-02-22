using Microsoft.EntityFrameworkCore;

namespace OMSSample.Models;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    public DbSet<OrderSingle> OrderSingles{ get; set;}
}