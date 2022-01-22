using Microsoft.AspNetCore.Mvc;
using StudentRewardsStore.Models;
using System;

namespace StudentRewardsStore.Controllers
{
    public class UserController : Controller
    {
        private readonly IStudentsRepository studentRepo;

        public UserController(IStudentsRepository studentRepo)
        {
            this.studentRepo = studentRepo;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult EnterPIN(Student user)
        {
            try
            {
                var record = studentRepo.ViewStudent(user.StudentID);
                if (user.StudentID == record.StudentID && record.PIN == null)
                {
                    var authenticate = new User() { Type = "student", StudentID = user.StudentID, AdminId = null, StoreID = user._OrganizationID };
                    return RedirectToAction("Store");
                }
                else
                {
                    ViewBag.Message = user.StudentID;
                    return View();
                }
            }
            catch (Exception)
            {
                return RedirectToAction("InvalidCredentials");
            }
                
        }
        public IActionResult CheckPIN(Student user)
        {
            try
            {
                var record = studentRepo.ViewStudent(user.StudentID);
                if (user.PIN == record.PIN)
                {
                    var authenticate = new User() { Type = "student", StudentID = user.StudentID, AdminId = null, StoreID = user._OrganizationID};
                    return RedirectToAction("Store");
                }
                else
                {
                    return RedirectToAction("InvalidCredentials");
                }
            }
            catch(Exception)
            {
                return RedirectToAction("InvalidCredentials");
            }
        }
        public IActionResult InvalidCredentials()
        {
            return View();
        }
        public IActionResult Store()
        {
            return View();
        }
    }
}
