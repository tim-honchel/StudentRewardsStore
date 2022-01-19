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
        public IActionResult LoginAdmin(string email)
        {
            try
            {
                var user = repo.LoginAdmin(email);
                return View(user);
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound");
            }
        }

        public IActionResult CheckPassword(string unhashed)
        {
            
            return RedirectToAction("WrongPassword");
        }
        public IActionResult RegisterAdmin(Admin admin)
        {
            repo.RegisterAdmin(admin);
            return RedirectToAction("Index");
        }
        public IActionResult NotFound()
        {
            return View();
        }
        public IActionResult WrongPassword()
        {
            return View();
        }
    }
}

