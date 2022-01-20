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
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        public IActionResult ViewAdmin()
        {
            return View();
        }
        public IActionResult LoginAdmin(string email, string unhashed)
        {
            try
            {
                var user = repo.LoginAdmin(email, unhashed);
                return View(user);
            }
            catch (Exception)
            {
                return RedirectToAction("InvalidCredentials");
            }
        }

        
        public IActionResult RegisterAdmin(Admin admin)
        {
            repo.RegisterAdmin(admin);
            return RedirectToAction("Index");
        }
        public IActionResult InvalidCredentials()
        {
            return View();
        }
        
        
    }
}

