using Microsoft.AspNetCore.Mvc;
using StudentRewardsStore.Models;

namespace StudentRewardsStore.Controllers
{
    public class OrganizationController : Controller
    {

        private readonly IOrganizationsRepository repo;
        public OrganizationController(IOrganizationsRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateStore()
        {
            return View();
        }
        public IActionResult SaveNewStore(Organization newStore)
        {
            repo.SaveNewStore(newStore);
            return RedirectToAction("Index");
        }

    }
}

