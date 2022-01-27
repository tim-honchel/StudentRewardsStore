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
            this.repo = repo; // data repository for the admin table
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login"); // admin login page
        }
        public IActionResult Login()
        {
            return View(); // admin login page
        }

        public IActionResult Register()
        {
            return View(); // new admin registration page
        }

        public IActionResult Overview(string email, string unhashed) // passes in the email and password from the login page
        {
            
            try
            {
                var authenticate = repo.CheckPassword(email, unhashed); // authenticates that the email and password match the database record
                var stores = repo.ListStores(authenticate.AdminID); // retrieves data for all stores associated with the admin
                var admin = repo.GetAdminID(email); // retrieves the admin's ID

                Authentication.Type = "admin";
                Authentication.StudentID = -1; // not a student
                Authentication.AdminID = admin.AdminID;
                Authentication.StoreID = -1; // no store has been selected yet

                return View(stores); // the admin overview page
            }
            catch (Exception)
            {
                return RedirectToAction("InvalidCredentials"); // redirects if the email and password could not be authenticated
            }
        }


        public IActionResult RegisterAdmin(Admin admin) // passes in data for a new admin from the register page
        {
            try
            {
                repo.RegisterAdmin(admin); // encrypts the password and writes the new admin data to the database
                return RedirectToAction("Login"); // returns to the login page
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
        public IActionResult NotSignedIn() // redirected when attempting to access a protected page without authorization
        {
            return View(); // displays message
        }

    }
}

