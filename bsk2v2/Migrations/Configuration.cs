namespace bsk2v2.Migrations
{
    using bsk2v2.Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            context.ControlLevels.AddOrUpdate(
                x => x.Level,
                new ControlLevel { Level = 1, Name = "Public" },
                new ControlLevel { Level = 2, Name = "Confidential" },
                new ControlLevel { Level = 3, Name = "Secret" },
                new ControlLevel { Level = 4, Name = "Top secret" });
        }
    }
}