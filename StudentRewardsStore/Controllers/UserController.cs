using Microsoft.AspNetCore.Mvc;
using StudentRewardsStore.Models;
using System;
using System.Collections.Generic;

namespace StudentRewardsStore.Controllers
{
    public class UserController : Controller
    {
        private readonly IStudentsRepository studentRepo;
        private readonly IOrganizationsRepository orgRepo;
        private readonly IPrizesRepository prizeRepo;

        public UserController(IStudentsRepository studentRepo, IOrganizationsRepository orgRepo, IPrizesRepository prizeRepo)
        {
            this.studentRepo = studentRepo;
            this.orgRepo = orgRepo;
            this.prizeRepo = prizeRepo;
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
                    Authentication.Type = "student";
                    Authentication.StudentID = user.StudentID;
                    Authentication.AdminID = -1;
                    Authentication.StoreID = studentRepo.ViewStudent(user.StudentID)._OrganizationID;
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
                    Authentication.Type = "student";
                    Authentication.StudentID = user.StudentID;
                    Authentication.AdminID = -1;
                    Authentication.StoreID = studentRepo.ViewStudent(user.StudentID)._OrganizationID;
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
            if (Authentication.Type == "student")
            {
                var student = studentRepo.ViewStudent(Authentication.StudentID);
                var store = orgRepo.OpenStore(Authentication.StoreID);
                StoreInfo.StudentName = student.StudentName;
                StoreInfo.Balance = student.Balance;
                StoreInfo.StoreName = store.Name;
                StoreInfo.StoreStatus = store.StoreStatus;
                StoreInfo.Currency = store.CurrencyName;
                var prizes = prizeRepo.ShowAvailablePrizes(Authentication.StoreID);
                return View(prizes);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public IActionResult ReviewOrder(IEnumerable<Prize> orders)
        {
            return View(orders);
        }
        public IActionResult Logout()
        {
            Authentication.Type = "";
            Authentication.StudentID = -1;
            Authentication.AdminID = -1;
            Authentication.StoreID = -1;
            StoreInfo.StudentName = "";
            StoreInfo.Balance = -1;
            StoreInfo.StoreName = "";
            StoreInfo.StoreStatus = "";
            StoreInfo.Currency = "";
            return RedirectToAction("Index", "Home");
        }
    }
}
