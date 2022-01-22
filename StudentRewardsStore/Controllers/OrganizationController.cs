using Microsoft.AspNetCore.Mvc;
using StudentRewardsStore.Models;
using System.Linq;
using System;

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
            repo.SaveNewStore(newStore); // adds the new store to the database
            var refreshedStore = repo.RefreshStore(newStore); // retrieves the store with its auto-generated store ID
            return RedirectToAction("Overview", new {id = refreshedStore.OrganizationID}); // redirects to the Store Overview page
        }
        public IActionResult Settings(int id) // receives store ID
        {
            if (Authentication.StoreID == id)
            {
                var store = repo.OpenStore(id);
                return View(store); ;
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin");
            }
        }
        public IActionResult UpdateStore(Organization store)
        {
            repo.UpdateStore(store);
            return RedirectToAction("Overview", new { id = store.OrganizationID });
        }
    }
}

