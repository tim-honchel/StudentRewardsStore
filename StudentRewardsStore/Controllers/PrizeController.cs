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
            this.repo = repo; // data repository for prizes table
        }
        public IActionResult Index(int id) // passes in prizeID from prize overview page
        {
            var prize = repo.ViewPrize(id); // retrieves data for the relevant prize
            if (Authentication.StoreID == prize._OrganizationID) // verifies the admin is authorized to view the prize's data
            {
                prize.StatusDropdown = new List<string>() { "show", "hide" }; // sets up dropdown list for display status
                prize.StatusDropdown.Remove(prize.DisplayStatus); // prevents duplicate values in dropdown list
                prize.PriceDropdown = new List<int>(); // sets up dropdown list for price
                for (int i = 0; i <= 1000; i++) // populates the dropdown list with values from 0 to 1,000
                {
                    prize.PriceDropdown.Add(i);
                }
                prize.PriceDropdown.Remove(prize.Price); // prevents duplicate values in dropdown list
                prize.InventoryDropdown = new List<int>(); // sets up dropdown list for inventory
                for (int i = 0; i <= 500; i++) // populates the dropdown list with values from 0 to 500
                {
                    prize.InventoryDropdown.Add(i);
                }
                prize.InventoryDropdown.Remove(prize.Inventory); // prevents duplicate values in dropdown list
                return View(prize); // the specific prize page
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin"); // redirects if the admin is not authorized or logged in
            }
        }
        public IActionResult Overview()
        {
            if (Authentication.Type == "admin" && Authentication.StoreID > 0 ) // authenticates that an admin is logged in
            { 
                var prizes = repo.ListPrizes(Authentication.StoreID); // retrieves data for all the store's prizes
                return View(prizes); // the prize overview page
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin"); // redirects if the admin is not authorized or logged in
            }
        }
        public IActionResult CreatePrize()
        {
            if (Authentication.Type == "admin" && Authentication.StoreID > 0) // authenticates that an admin is logged in
            {
                var prize = new Prize();
                prize.StatusDropdown = new List<string>() { "show", "hide" }; // sets up dropdown list for display status
                prize.PriceDropdown = new List<int>(); // sets up dropdown list for price
                for (int i = 0; i <= 1000; i++) // populates dropdown list with values from 0 to 1,000
                {
                    prize.PriceDropdown.Add(i);
                }
                prize.InventoryDropdown = new List<int>(); // sets up dropdown list for inventory
                for (int i = 0; i <= 500; i++) // populates dropdown list with values from 0 to 500
                {
                    prize.InventoryDropdown.Add(i);
                }
                return View(prize); // create prize page
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin"); // redirects if an admin is not logged in
            }
        }
        public IActionResult SaveNewPrize(Prize newPrize) // passes in data for new prize from the create prize page
        {
            try
            {
                newPrize.ImageWidth = 150; // sets the image size to a 150x150 pixel square
                newPrize.ImageHeight = 150;
                repo.AddPrize(newPrize); // writes the prize data to the database
                return RedirectToAction("Overview"); // prize overview page
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
                prize.ImageWidth = 150; // sets the image size to a 150x150 pixel square
                prize.ImageHeight = 150;
                repo.UpdatePrize(prize); // updates the prize data to the database
                return RedirectToAction("Overview"); // the prize overview page
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home"); // redirects if there is an error writing to the database
            }
        }
    }
}
