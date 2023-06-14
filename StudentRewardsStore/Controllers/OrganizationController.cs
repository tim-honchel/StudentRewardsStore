using Microsoft.AspNetCore.Mvc;
using StudentRewardsStore.Models;
using System.Linq;
using System;
using System.Collections.Generic;

namespace StudentRewardsStore.Controllers
{
    public class OrganizationController : Controller
    {

        private readonly IOrganizationsRepository orgRepo;
        private readonly IPrizesRepository prizeRepo;
        private readonly IAdminsRepository adminRepo;
        public OrganizationController(IOrganizationsRepository orgRepo, IPrizesRepository prizeRepo, IAdminsRepository adminRepo)
        {
            this.orgRepo = orgRepo;
            this.prizeRepo = prizeRepo;
            this.adminRepo = adminRepo;
        }

        public IActionResult Index(int id) // receives store ID
        {
            return RedirectToAction("Overview", new { id = id });
        }
        
        public IActionResult Overview(int id) // receives store ID
        {
            if (Authentication.Type == "demo admin")
            {
                id = 1;
            }
            if (id > 0)
            {
                var store = orgRepo.OpenStore(id); // retrieves the relevant store
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
            if (Authentication.Type == "admin" || Authentication.Type == "demo admin") // checks that an admin is logged in
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
                orgRepo.SaveNewStore(newStore); // adds the new store to the database
                var refreshedStore = orgRepo.RefreshStore(newStore); // retrieves the store with its auto-generated store ID
                Authentication.LastAction = DateTime.Now;
                adminRepo.LoginAdmin();
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
                var store = orgRepo.OpenStore(id);
                store.StatusDropdown = new List<string>() { "closed", "open" };
                store.StatusDropdown.Remove(store.StoreStatus);
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
                orgRepo.UpdateStore(store);
                Authentication.LastAction = DateTime.Now;
                adminRepo.LoginAdmin();
                return RedirectToAction("Overview", new { id = store.OrganizationID });
            } 
            catch (Exception)
            {
                return RedirectToAction("Error", "Home"); // redirects if there is an error writing to the database
            }
        }
        public IActionResult Demo()
        {
            Authentication.Type = "demo admin";
            Authentication.StudentID = -1;
            Authentication.AdminID = 1;
            Authentication.StoreID = 1;
            var demoStore = new Organization();
            demoStore = orgRepo.OpenStore(1);
            demoStore.OrganizationID = 1;
            demoStore.Name = "Demo Store";
            demoStore.CurrencyName = "Demo Dollars";
            demoStore.StoreStatus = "open";
            demoStore._AdminID = 1;
            orgRepo.LoadDemoStore(demoStore);
            prizeRepo.LoadDemoPrizes();
            var demoAdmin = adminRepo.GetAdminID("testing@gmail.com");
            Authentication.LastAction = DateTime.Now;
            if (demoAdmin.LoggedIn == "yes" && demoAdmin.LastAction != null && Authentication.LastAction.Subtract(demoAdmin.LastAction).Minutes < 30)
            {
                Authentication.MultipleUsers = true;
            }
            else
            {
                Authentication.MultipleUsers = false;
            }
            adminRepo.LoginAdmin();
            return RedirectToAction("Overview", 1);
        }
    }
}

