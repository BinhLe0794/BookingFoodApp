using AdminApp.Entities;
using AdminApp.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AdminApp.Config;

public class ApplicationDbContext : IdentityDbContext<Account>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        var modified = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
        foreach (var item in modified)
        {
            if (item.Entity is not ITracking changedOrAddedItem) continue;
            if (item.State == EntityState.Added)
            {
                changedOrAddedItem.CreatedAt = DateTime.Now;
            }
            else
            {
                changedOrAddedItem.ModifiedAt = DateTime.Now;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>().Property(x => x.Id).HasMaxLength(50).IsUnicode(false);
        builder.Entity<Account>().Property(x => x.Id).HasMaxLength(50).IsUnicode(false);
        builder.Entity<Dish>().HasOne<Order>(x => x.Order)
                             .WithMany(x => x.Dishes)
                             .HasForeignKey(x => x.OrderId);
        
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName!.StartsWith("AspNet")) entityType.SetTableName(tableName.Substring(6));
        }
    }

    public DbSet<Dish> Dishes { set; get; } = null!;
    public DbSet<Order> Orders { set; get; } = null!;
}