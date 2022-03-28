using Microsoft.AspNetCore.Mvc;
using StudentRewardsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentRewardsStore.Controllers
{
    public class UserController : Controller
    {
        private readonly IStudentsRepository studentRepo;
        private readonly IOrganizationsRepository orgRepo;
        private readonly IPrizesRepository prizeRepo;
        private readonly IOrdersRepository ordersRepo;

        public UserController(IStudentsRepository studentRepo, IOrganizationsRepository orgRepo, IPrizesRepository prizeRepo, IOrdersRepository orderRepo)
        {
            this.studentRepo = studentRepo;
            this.orgRepo = orgRepo;
            this.prizeRepo = prizeRepo;
            this.ordersRepo = orderRepo;
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
                    StoreInfo.CurrentOrder.Clear();
                    StoreInfo.CartMessage = "";
                    StoreInfo.StudentStatus = record.Status;
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
                    StoreInfo.CurrentOrder.Clear();
                    StoreInfo.CartMessage = "";
                    StoreInfo.StudentStatus = record.Status;
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
            if (Authentication.Type == "student" || Authentication.Type == "demo student")
            {
                var student = studentRepo.ViewStudent(Authentication.StudentID);
                var store = orgRepo.OpenStore(Authentication.StoreID);
                StoreInfo.StudentName = student.StudentName;
                StoreInfo.Balance = student.Balance;
                StoreInfo.StoreName = store.Name;
                StoreInfo.StoreStatus = store.StoreStatus;
                StoreInfo.Currency = store.CurrencyName;
                var prizes = prizeRepo.ShowAvailablePrizes(Authentication.StoreID);
                foreach (Prize item in prizes)
                {
                    item.QuantitySelections = new List<int> { };
                    for (int num = 0; num <= item.Inventory; num++)
                    {
                        item.QuantitySelections.Add(num);
                    }
                }
                return View(prizes);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public IActionResult AddToCart(Prize addToCart)
        {
            addToCart.Cost = addToCart.Quantity * addToCart.Price;
            if (addToCart.Quantity + StoreInfo.CurrentOrder.Where(x=>x.PrizeID == addToCart.PrizeID).Sum(x=>x.Quantity) > addToCart.Inventory)
            {
                StoreInfo.CartMessage = $"Did not add {addToCart.PrizeName} because aren't enough left in the store.";
            }
            else if (addToCart.Cost + StoreInfo.CurrentOrder.Sum(x=>x.Cost) > StoreInfo.Balance)
            {
                StoreInfo.CartMessage = $"Did not add {addToCart.PrizeName} because you don't have enough {StoreInfo.Currency}.";
            }
            else if (addToCart.Quantity == 0)
            {
                StoreInfo.CartMessage = $"Did not add {addToCart.PrizeName} because you forgot to choose a quantity.";
            }
            else
            {
                StoreInfo.CartMessage = "";
                StoreInfo.CurrentOrder.Add(addToCart);
            }
            return RedirectToAction("Store");
        }
        public IActionResult SubmitOrder()
        {
            var finalOrder = new List<Order>();
           
            foreach (var item in StoreInfo.CurrentOrder)
            {
                finalOrder.Add(createOrder(item));
            }
            if (Authentication.Type != "demo student")
            {
                ordersRepo.SaveNewOrders(finalOrder);
            }
            StoreInfo.CurrentOrder.Clear();
            return View(finalOrder);
        }
        public Order createOrder(Prize item)
        {
            var individualItem = new Order();
            individualItem.OrderDate = DateTime.Now;
            individualItem.PrizeName = item.PrizeName;
            individualItem.Quantity = item.Quantity;
            individualItem.Price = item.Price;
            individualItem.Cost = item.Cost;
            individualItem.FulfillmentStatus = "unfulfilled";
            individualItem._StudentID = Authentication.StudentID;
            individualItem._Organization_ID_ = Authentication.StoreID;
            individualItem._PrizeID = item.PrizeID;
            return individualItem;
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
            StoreInfo.CurrentOrder.Clear();
            StoreInfo.CartMessage = "";
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Demo()
        {
            Authentication.Type = "demo student";
            Authentication.StudentID = 1;
            Authentication.AdminID = -1;
            Authentication.StoreID = 1;
            StoreInfo.CurrentOrder.Clear();
            StoreInfo.CartMessage = "";
            StoreInfo.StudentStatus = "active";
            
            var demoStudent = new Student();
            demoStudent.StudentID = 1;
            demoStudent.StudentName = "Maria";
            demoStudent.Status = "active";
            demoStudent.Balance = 36;
            studentRepo.LoadDemoStudent(demoStudent);

            var demoStore = new Organization();
            demoStore.OrganizationID = 1;
            demoStore.Name = "Demo Store";
            demoStore.CurrencyName = "Demo Dollars";
            demoStore.StoreStatus = "open";
            demoStore._AdminID = 1;
            orgRepo.LoadDemoStore(demoStore);

            prizeRepo.LoadDemoPrizes();

            return RedirectToAction("Store");
        }
        
    }
}
