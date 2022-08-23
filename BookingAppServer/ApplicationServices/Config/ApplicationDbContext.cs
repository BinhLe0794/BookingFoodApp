using ApplicationServices.Entities;
using ApplicationServices.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApplicationServices.Config;

public class ApplicationDbContext : IdentityDbContext<Account>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Dish> Dishes { set; get; } = null!;
    public DbSet<Order> Orders { set; get; } = null!;

    public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var modified = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
        foreach (var item in modified)
        {
            if (item.Entity is not ITracking changedOrAddedItem) continue;
            if (item.State == EntityState.Added)
                changedOrAddedItem.CreatedAt = DateTime.Now;
            else
                changedOrAddedItem.ModifiedAt = DateTime.Now;
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>().Property(x => x.Id).HasMaxLength(50).IsUnicode(false);
        builder.Entity<Account>().Property(x => x.Id).HasMaxLength(50).IsUnicode(false);

        builder.Entity<OrderDetail>().HasOne(x => x.Order)
            .WithMany(x => x.OrderDetails)
            .HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);

        builder.Entity<OrderDetail>().HasOne(x => x.Dish)
            .WithMany(x => x.OrderDetails)
            .HasForeignKey(x => x.DishId).OnDelete(DeleteBehavior.Cascade);

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName!.StartsWith("AspNet")) entityType.SetTableName(tableName.Substring(6));
        }
    }
}