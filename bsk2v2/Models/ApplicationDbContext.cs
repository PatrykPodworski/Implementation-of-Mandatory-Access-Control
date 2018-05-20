using bsk2v2.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<ControlLevel> ControlLevels { get; set; }
    public DbSet<Recipe> Recipes { get; set; }

    public ApplicationDbContext()
        : base("DefaultConnection", throwIfV1Schema: false)
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
    }

    public static ApplicationDbContext Create()
    {
        return new ApplicationDbContext();
    }
}