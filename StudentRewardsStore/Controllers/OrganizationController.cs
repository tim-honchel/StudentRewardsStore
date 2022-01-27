using Microsoft.AspNetCore.Mvc;
using StudentRewardsStore.Models;
using System.Linq;
using System;
using System.Collections.Generic;

namespace StudentRewardsStore.Controllers
{
    public class OrganizationController : Controller
    {

        private readonly IOrganizationsRepository repo;
        public OrganizationController(IOrganizationsRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index(int id) // receives store ID
        {
            return RedirectToAction("Overview", new { id = id });
        }
        
        public IActionResult Overview(int id) // receives store ID
        {
            if (id > 0)
            {
                var store = repo.OpenStore(id); // retrieves the relevant store
                if (Authentication.AdminID == store._AdminID) // authenticates that the admin ID matches the store's admin ID
                {
                    Authentication.StoreID = store.OrganizationID;
                    return View(store); // the Store Overview page
                }
                else
                {
                    return RedirectToAction("NotSignedIn", "Admin"); // displays message if authentication failed
                }
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin"); // displays message if admin tried to access store dashboard prior to selecting a specific store
            }
        }
        
      
        public IActionResult CreateStore()
        {
            if (Authentication.Type == "admin") // checks that an admin is logged in
            {
                return View(); // the Create Store page
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin"); // displays message if no admin is logged in
            }
        }
        public IActionResult SaveNewStore(Organization newStore) // receives data for a new store
        {
            try
            {
                newStore.StoreStatus = "closed"; // default value
                repo.SaveNewStore(newStore); // adds the new store to the database
                var refreshedStore = repo.RefreshStore(newStore); // retrieves the store with its auto-generated store ID
                return RedirectToAction("Overview", new { id = refreshedStore.OrganizationID }); // redirects to the Store Overview page
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home"); // redirects if there is an error writing to the database
            }
        }
        public IActionResult Settings(int id) // receives store ID
        {
            if (Authentication.StoreID == id)
            {
                var store = repo.OpenStore(id);
                store.StatusDropdown = new List<string>() { "closed", "open" };
                return View(store); ;
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin");
            }
        }
        public IActionResult UpdateStore(Organization store)
        {
            try {
                repo.UpdateStore(store);
                return RedirectToAction("Overview", new { id = store.OrganizationID });
            } 
            catch (Exception)
            {
                return RedirectToAction("Error", "Home"); // redirects if there is an error writing to the database
            }
        }
    }
}

