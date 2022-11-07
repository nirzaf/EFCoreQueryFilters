using Bogus;

using Microsoft.EntityFrameworkCore;

namespace EFCoreQueryFilters.Models;

public class EventDbContext : DbContext
{
    public EventDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Event> Events { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var ids = 1;
        var eventNew = new Faker<Event>()
            .RuleFor(m=>m.Id, f=>ids++)
            .RuleFor(m=>m.CreatedAt , f=> DateTime.UtcNow)
            .RuleFor(m=>m.UpdatedAt , f=> DateTime.UtcNow)
            .RuleFor(m=>m.CreatedBy , f=> f.Person.UserName)
            .RuleFor(m=>m.UpdatedBy , f=> f.Person.UserName)
            .RuleFor(m=>m.eventCode, f=>f.Random.AlphaNumeric(15).ToUpper())
            .RuleFor(m=>m.eventName, f=>f.Name.FirstName())
            .RuleFor(m=>m.eventDescription, f=>f.Lorem.Sentence())
            .RuleFor(m=>m.eventLocation, f=>f.Address.City())
            .RuleFor(m=>m.eventStartDate, f=>f.Date.Past())
            .RuleFor(m=>m.eventEndDate, f=>f.Date.Soon())
            .RuleFor(m=>m.eventStatus, f=> f.PickRandom("Active", "Inactive", "Cancelled", "Completed"));
        
        modelBuilder.Entity<Event>().HasData(eventNew.GenerateBetween(500, 1000));
        
        modelBuilder.Entity<Event>()
            .HasQueryFilter(e => e.IsDeleted == false);
        
        base.OnModelCreating(modelBuilder);
    }
    
    // Add Create and Update Auditing before saving changes
    public override int SaveChanges()
    {
        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            if (entry is { Entity: Event entity })
            {
                switch (entry)
                {
                    case { State: EntityState.Added }:
                        entity.CreatedAt = DateTime.UtcNow;
                        entity.CreatedBy = CurrentUser.Name;
                        break;
                    case { State: EntityState.Modified }:
                        entity.UpdatedAt = DateTime.UtcNow;
                        entity.UpdatedBy = CurrentUser.Name;
                        break;
                }
            }
        }
        return base.SaveChanges();
    }
    
    // Add Create and Update Auditing before SaveChangesAsync
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            if (entry is { Entity: Event entity })
            {
                switch (entry)
                {
                    case { State: EntityState.Added }:
                        entity.CreatedAt = DateTime.UtcNow;
                        entity.CreatedBy = CurrentUser.Name;
                        break;
                    case { State: EntityState.Modified }:
                        entity.UpdatedAt = DateTime.UtcNow;
                        entity.UpdatedBy = CurrentUser.Name;
                        break;
                }
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
    
}