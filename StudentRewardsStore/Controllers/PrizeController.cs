using Microsoft.AspNetCore.Mvc;
using System;
using StudentRewardsStore.Models;

namespace StudentRewardsStore.Controllers
{
    public class PrizeController : Controller
    {
        private readonly IPrizesRepository repo;
        public PrizeController(IPrizesRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index(int id) // receives prize ID
        {
            
            var prize = repo.ViewPrize(id); // retrieves the relevant prize
            if (Authentication.StoreID == prize._OrganizationID)
            {
                return View(prize); // the specific prize page
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin");
            }
        }
        public IActionResult Overview()
        {
            if (Authentication.Type == "admin" && Authentication.StoreID > 0 )
            { 
                var prizes = repo.ListPrizes(Authentication.StoreID); // retrieves all the store's prizes
                return View(prizes); // the prize overview page
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin");
            }
        }
        public IActionResult CreatePrize()
        {
            if (Authentication.Type == "admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin");
            }
        }
        public IActionResult SaveNewPrize(Prize newPrize)
        {
            repo.AddPrize(newPrize);
            ViewBag.Message = TempData["AuthenticateOrganizationID"];
            TempData["AuthenticateOrganizationID"] = newPrize._OrganizationID;
            return RedirectToAction("Overview");
        }
        public IActionResult UpdatePrize(Prize prize)
        {
            repo.UpdatePrize(prize);
            return RedirectToAction("Overview");
        }
    }
}
