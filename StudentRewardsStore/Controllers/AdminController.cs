using Microsoft.AspNetCore.Mvc;
using StudentRewardsStore.Models;
using System;

namespace StudentRewardsStore.Controllers
{
    public class AdminController : Controller
    {

        private readonly IAdminsRepository repo;
        
        public AdminController(IAdminsRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login"); // the admin login page
        }
        public IActionResult Login()
        {
            return View(); // the admin login page
        }

        public IActionResult Register()
        {
            return View(); // the new admin registration page
        }

        public IActionResult Overview(string email, string unhashed) // receives the email and password from the login page
        {
            
            try
            {
                var authenticate = repo.CheckPassword(email, unhashed); // authenticates that the mail and password match the database record
                var stores = repo.ListStores(authenticate.AdminID); // retrieves all the stores owned by the admin
                var admin = repo.GetAdminID(email); // retrieves the admin

                Authentication.Type = "admin";
                Authentication.StudentID = -1;
                Authentication.AdminID = admin.AdminID;
                Authentication.StoreID = -1;

                return View(stores); // the admin overview page
            }
            catch (Exception)
            {
                return RedirectToAction("InvalidCredentials"); // redirects if the email and password could not be authenticated
            }
        }


        public IActionResult RegisterAdmin(Admin admin) // receives data for a new admin
        {
            try
            {
                repo.RegisterAdmin(admin); // encrypts the password and adds new admin record to database
                return RedirectToAction("Login"); // return to the login page
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home"); // redirects if there is an error writing to the database
            }
        }
        public IActionResult InvalidCredentials() // redirected when the email and/or password do not match the database
        {
            return View(); // displays message
        }
        public IActionResult NotSignedIn() // redirected when attempting to access a protected page
        {
            return View(); // displays message
        }

    }
}

