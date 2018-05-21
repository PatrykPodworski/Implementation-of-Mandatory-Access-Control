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
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Recipe")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "Author")]
        public string AuthorName { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public RecipeDetailsViewModel(Recipe recipe)
        {
            Id = recipe.Id;
            Name = recipe.Name;
            Text = recipe.Text;
            AuthorName = recipe.Author.Name;
            AuthorId = recipe.AuthorId;
        }
    }

    public class RecipeCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Recipe")]
        public string Text { get; set; }

        [Required]
        public int ClassificationLevel { get; set; }

        public ICollection<SelectListItem> ClassificationLevels { get; set; }

        public RecipeCreateViewModel()
        {
        }

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

    public class RecipeEditViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Recipe")]
        public string Text { get; set; }

        [Required]
        public int ClassificationLevel { get; set; }

        public ICollection<SelectListItem> ClassificationLevels { get; set; }

        public RecipeEditViewModel()
        {
        }

        public RecipeEditViewModel(Recipe recipe, ICollection<ControlLevel> classificationLevels)
        {
            Id = recipe.Id;
            Name = recipe.Name;
            Text = recipe.Text;
            ClassificationLevel = ClassificationLevel;
            ClassificationLevels = classificationLevels
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Level.ToString()
                })
                .ToList();
        }
    }

    public class RecipeDeleteViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Recipe")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "Author")]
        public string AuthorName { get; set; }

        [Required]
        public int ClassificationLevel { get; set; }

        public RecipeDeleteViewModel()
        {
        }

        public RecipeDeleteViewModel(Recipe recipe)
        {
            Id = recipe.Id;
            Name = recipe.Name;
            Text = recipe.Text;
            AuthorName = recipe.Author.Name;
            ClassificationLevel = recipe.ClassificationLevel.Level;
        }
    }
}