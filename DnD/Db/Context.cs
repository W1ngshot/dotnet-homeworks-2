using Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Db;

public class Context : DbContext
{
    public DbSet<Monster> Monsters { get; set; }
    
    public Context(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Monster>().HasData(MonsterService.GetAll());
        base.OnModelCreating(modelBuilder);
    }
}