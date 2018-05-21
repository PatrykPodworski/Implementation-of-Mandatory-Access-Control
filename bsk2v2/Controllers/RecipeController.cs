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

        // POST: Recipe/Create
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

        // GET: Recipe/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Recipe/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Recipe/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Recipe/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}