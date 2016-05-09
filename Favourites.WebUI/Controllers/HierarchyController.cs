using System;
using System.Web.Mvc;
using Favourites.Domain;
using Favourites.Repository;
using Favourites.WebUI.Models;

namespace Favourites.WebUI.Controllers
{
    public class HierarchyController : Controller
    {
        private readonly IHierarchyRepository<Favourite> repository;

        public HierarchyController()
        {
            repository = new HierarchyRepository();
        }

        // GET: Hierarchy
        public ActionResult Index()
        {
            return View(repository.GetAll());
        }

        public ActionResult Details(Guid level)
        {
            return View(repository.GetForLevel(level));
        }

        public ActionResult AddLevel(Guid? parent)
        {
            return View(new LevelViewModel { Parent = parent });
        }

        [HttpPost]
        public ActionResult AddLevel(LevelViewModel model)
        {
            repository.AddChild(model.Parent, model.Description);
            return RedirectToAction("Index");
        }

        public ActionResult AddFavourite(Guid owner)
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFavourite(FavouriteViewModel model)
        {
            repository.AddFavourite(model.Owner, model.Sedol);
            return RedirectToAction("Index");
        }
    }
}