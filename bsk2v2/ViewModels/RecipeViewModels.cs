using bsk2v2.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace bsk2v2.ViewModels
{
    public class RecipeIndexViewModel
    {
        public ICollection<RecipeIndexPartModel> Recipes { get; set; }

        public RecipeIndexViewModel(ICollection<Recipe> recipes)
        {
            Recipes = recipes.Select(x => new RecipeIndexPartModel(x))
                .ToList();
        }
    }

    public class RecipeIndexPartModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string AuthorName { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public RecipeIndexPartModel(Recipe recipe)
        {
            Id = recipe.Id;
            Name = recipe.Name;
            AuthorName = recipe.Author.Name;
            AuthorId = recipe.AuthorId;
        }
    }

    public class RecipeDetailsViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string AuthorName { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public RecipeDetailsViewModel(Recipe recipe)
        {
            Name = recipe.Name;
            AuthorName = recipe.Author.Name;
            AuthorId = recipe.AuthorId;
        }
    }

    public class RecipeCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int ClassificationLevel { get; set; }

        [Required]
        public ICollection<SelectListItem> ClassificationLevels { get; set; }

        public RecipeCreateViewModel(ICollection<ControlLevel> classificationLevels)
        {
            ClassificationLevels = classificationLevels
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Level.ToString()
                })
                .ToList();
        }
    }
}