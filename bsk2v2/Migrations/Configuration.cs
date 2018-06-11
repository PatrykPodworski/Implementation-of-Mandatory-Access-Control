namespace bsk2v2.Migrations
{
    using bsk2v2.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        private string _sampleText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam nec justo elit. Maecenas fringilla convallis massa. Integer sed orci porttitor, suscipit ante vel, elementum quam. Curabitur vel posuere diam. Proin a metus id mi tristique lobortis. Donec laoreet elit molestie metus condimentum, ac ultricies mauris finibus. In id urna magna. Ut non enim in dui dictum tincidunt.";

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var controlLevels = new List<ControlLevel>
            {
                new ControlLevel { Id = 0, Level = 1, Name = "Public" },
                new ControlLevel { Id = 1, Level = 2, Name = "Confidential" },
                new ControlLevel { Id = 2, Level = 3, Name = "Secret" },
                new ControlLevel { Id = 3, Level = 4, Name = "Top secret" }
            };

            context.ControlLevels.AddRange(controlLevels);

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var publicUserInfo = new User { Name = "PublicUser", Email = "PublicUser@gmail.com", CleranceLevelId = controlLevels[0].Id };
            var publicUser = new ApplicationUser { UserName = "PublicUser", Email = "PublicUser@gmail.com", UserInfo = publicUserInfo };
            userManager.Create(publicUser, "public");

            var confidentialUserInfo = new User { Name = "ConfidentialUser", Email = "ConfidentialUser@gmail.com", CleranceLevelId = controlLevels[1].Id };
            var confidentialUser = new ApplicationUser { UserName = "ConfidentialUser", Email = "ConfidentialUser@gmail.com", UserInfo = confidentialUserInfo };
            userManager.Create(confidentialUser, "confidential");

            var secretUserInfo = new User { Name = "SecretUser", Email = "SecretUser@gmail.com", CleranceLevelId = controlLevels[2].Id };
            var secretUser = new ApplicationUser { UserName = "SecretUser", Email = "SecretUser@gmail.com", UserInfo = secretUserInfo };
            userManager.Create(secretUser, "secret");

            var topSecretUserInfo = new User { Name = "TopSecretUser", Email = "TopSecretUser@gmail.com", CleranceLevelId = controlLevels[3].Id };
            var topSecretUser = new ApplicationUser { UserName = "TopSecretUser", Email = "TopSecretUser@gmail.com", UserInfo = topSecretUserInfo };
            userManager.Create(topSecretUser, "topSecret");

            context.Recipes.AddRange(new List<Recipe>
            {
                new Recipe{Name = "Public Recipe", Text = _sampleText, Author = publicUserInfo, ClassificationLevel = publicUserInfo.CleranceLevel },
                new Recipe{Name = "Confidential Recipe", Text = _sampleText, Author = confidentialUserInfo, ClassificationLevel = confidentialUserInfo.CleranceLevel },
                new Recipe{Name = "Secret Recipe", Text = _sampleText, Author = secretUserInfo, ClassificationLevel = secretUserInfo.CleranceLevel },
                new Recipe{Name = "Top Secret Recipe", Text = _sampleText, Author = topSecretUserInfo, ClassificationLevel = topSecretUserInfo.CleranceLevel },
            });
        }
    }
}