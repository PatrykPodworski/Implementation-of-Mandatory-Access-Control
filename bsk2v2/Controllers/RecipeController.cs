using bsk2v2.Services;
using bsk2v2.ViewModels;
using System.Web.Mvc;

namespace bsk2v2.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new ApplicationDbContext())
            {
                var recipeService = new RecipeService(context, HttpContext);
                var recipes = recipeService.GetAllAvailableRecipes();

                var viewModel = new RecipeIndexViewModel(recipes);

                return View(viewModel);
            }
        }

        public ActionResult Details(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var recipeService = new RecipeService(context, HttpContext);
                var recipe = recipeService.GetRecipe(id);

                if (recipe == null)
                {
                    return HttpNotFound();
                }

                var viewModel = new RecipeDetailsViewModel(recipe);
                return View(viewModel);
            }
        }

        public ActionResult Create()
        {
            using (var context = new ApplicationDbContext())
            {
                var userService = new UserService(context, HttpContext);
                var classificationLevels = userService.GetAvailableClassificationLevels();
                var viewModel = new RecipeCreateViewModel(classificationLevels);
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Create(RecipeCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var recipeService = new RecipeService(context, HttpContext);
                    recipeService.Add(model);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var recipeService = new RecipeService(context, HttpContext);
                var recipe = recipeService.GetRecipe(id);

                if (recipe == null)
                {
                    return HttpNotFound();
                }

                var userService = new UserService(context, HttpContext);
                var classificationLevels = userService.GetAvailableClassificationLevels();
                var viewModel = new RecipeEditViewModel(recipe, classificationLevels);
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(RecipeEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var recipeService = new RecipeService(context, HttpContext);
                    recipeService.Edit(model);
                    context.SaveChanges();

                    var recipe = recipeService.GetRecipe(model.Id);

                    if (recipe == null)
                    {
                        return RedirectToAction("Index");
                    }

                    return RedirectToAction("Details", new { id = model.Id });
                }
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var recipeService = new RecipeService(context, HttpContext);
                var recipe = recipeService.GetRecipe(id);

                if (recipe == null)
                {
                    return HttpNotFound();
                }

                var viewModel = new RecipeDeleteViewModel(recipe);
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Delete(RecipeDeleteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var recipeService = new RecipeService(context, HttpContext);
                    recipeService.Delete(model);
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View(model);
            }
        }
    }
}