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
            
            int authenticate = Convert.ToInt32(TempData["authenticateOrganizationID"]); // stores organization ID from previous page
            var prize = repo.ViewPrize(id); // retrieves the relevant prize
            if (authenticate == prize._OrganizationID)
            {
                ViewBag.Message = authenticate; //saves the organization ID for future authentication purposes
                return View(prize); // the specific prize page
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin");
            }
        }
        public IActionResult Overview()
        {
            try
            {
                int authenticate = Convert.ToInt32(TempData["authenticateOrganizationID"]); // stores organization ID from previous page
                var prizes = repo.ListPrizes(authenticate); // retrieves all the store's prizes
                ViewBag.Message = authenticate; // saves the organization ID for future authentication purposes
                return View(prizes); // the prize overview page
            }
            catch (Exception)
            {
                return RedirectToAction("NotSignedIn", "Admin");
            }
        }
        public IActionResult CreatePrize()
        {
            ViewBag.Message = TempData["AuthenticateOrganizationID"];
            return View();
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
