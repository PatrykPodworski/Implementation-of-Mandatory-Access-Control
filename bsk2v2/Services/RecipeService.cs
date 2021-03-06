﻿using bsk2v2.Models;
using bsk2v2.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace bsk2v2.Services
{
    public class RecipeService
    {
        private readonly ApplicationDbContext _context;
        private HttpContextBase _httpContext;

        public RecipeService(ApplicationDbContext context, HttpContextBase httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public ICollection<Recipe> GetAllAvailableRecipes()
        {
            var userService = new UserService(_context, _httpContext);
            var userCleranceLevel = userService.GetCurrentUserCleranceLevel();

            return _context.Recipes
                .Where(x => x.ClassificationLevel.Level <= userCleranceLevel.Level)
                .ToList();
        }

        public Recipe GetRecipe(int id)
        {
            var userService = new UserService(_context, _httpContext);
            var userCleranceLevel = userService.GetCurrentUserCleranceLevel();

            return _context.Recipes
                .FirstOrDefault(x => x.Id == id &&
                    x.ClassificationLevel.Level <= userCleranceLevel.Level);
        }

        public void Add(RecipeCreateViewModel model)
        {
            var controlLevelService = new ControlLevelService(_context);
            var classificationLevelId = controlLevelService.GetIdByLevel(model.ClassificationLevel);

            var userService = new UserService(_context, _httpContext);
            var authorId = userService.GetCurrentUserId();

            var recipe = new Recipe
            {
                Name = model.Name,
                Text = model.Text,
                ClassificationLevelId = classificationLevelId,
                AuthorId = authorId
            };

            _context.Recipes.Add(recipe);
        }

        internal void Edit(RecipeEditViewModel model)
        {
            var userService = new UserService(_context, _httpContext);
            var cleranceLevel = userService.GetCurrentUserCleranceLevel();

            var recipe = GetById(model.Id);

            if (cleranceLevel.Level < recipe.ClassificationLevel.Level)
            {
                return;
            }

            var controlLevelService = new ControlLevelService(_context);
            var classificationLevelId = controlLevelService.GetIdByLevel(model.ClassificationLevel);

            var authorId = userService.GetCurrentUserId();

            recipe.Id = model.Id;
            recipe.Name = model.Name;
            recipe.Text = model.Text;
            recipe.ClassificationLevelId = classificationLevelId;
            recipe.AuthorId = authorId;

            _context.Entry(recipe).State = EntityState.Modified;
        }

        private Recipe GetById(int id)
        {
            return _context.Recipes.SingleOrDefault(x => x.Id == id);
        }

        internal void Delete(RecipeDeleteViewModel model)
        {
            var userService = new UserService(_context, _httpContext);
            var userCleranceLevel = userService.GetCurrentUserCleranceLevel();

            if (userCleranceLevel.Level < model.ClassificationLevel)
            {
                return;
            }

            var recipe = new Recipe { Id = model.Id };
            _context.Recipes.Attach(recipe);
            _context.Entry(recipe).State = EntityState.Deleted;
        }
    }
}