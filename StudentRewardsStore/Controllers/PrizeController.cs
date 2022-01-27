using Microsoft.AspNetCore.Mvc;
using System;
using StudentRewardsStore.Models;
using System.Collections.Generic;

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
                prize.StatusDropdown = new List<string>() { "show", "hide" };
                prize.PriceDropdown = new List<int>();
                for (int i = 0; i <= 1000; i++)
                {
                    prize.PriceDropdown.Add(i);
                }
                prize.InventoryDropdown = new List<int>();
                for (int i = 0; i <= 500; i++)
                {
                    prize.InventoryDropdown.Add(i);
                }
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
                var prize = new Prize();
                prize.StatusDropdown = new List<string>() { "show", "hide" };
                prize.PriceDropdown = new List<int>();
                for (int i = 0; i <= 1000; i++)
                {
                    prize.PriceDropdown.Add(i);
                }
                prize.InventoryDropdown = new List<int>();
                for (int i = 0; i <= 500; i++)
                {
                    prize.InventoryDropdown.Add(i);
                }
                return View(prize);
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin");
            }
        }
        public IActionResult SaveNewPrize(Prize newPrize)
        {
            try
            {
                newPrize.ImageWidth = 150;
                newPrize.ImageHeight = 150;
                repo.AddPrize(newPrize);
                return RedirectToAction("Overview");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home"); // redirects if there is an error writing to the database
            }
        }
        public IActionResult UpdatePrize(Prize prize)
        {
            try
            {
                prize.ImageWidth = 150;
                prize.ImageHeight = 150;
                repo.UpdatePrize(prize);
                return RedirectToAction("Overview");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home"); // redirects if there is an error writing to the database
            }
        }
    }
}
