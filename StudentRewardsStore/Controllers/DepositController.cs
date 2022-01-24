using Microsoft.AspNetCore.Mvc;
using System;
using StudentRewardsStore.Models;

namespace StudentRewardsStore.Controllers
{
    public class DepositController : Controller
    {
        private readonly IDepositsRepository repo;
        public DepositController(IDepositsRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index(int id)
        {
            var deposit = repo.ViewDeposit(id); // retrieves the relevant deposit
            if (Authentication.StoreID == deposit._Organization_ID)
            {
                return View(deposit); // the specific deposit page
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin");
            }
        }
        public IActionResult Overview()
        {
            if (Authentication.Type == "admin" && Authentication.StoreID > 0)
            {
                var deposits = repo.ShowAllDeposits(Authentication.StoreID); // retrieves all the store's deposits
                return View(deposits); // the deposits overview page
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin");
            }
        }
        public IActionResult UpdateDeposit(Deposit deposit)
        {
            repo.UpdateDeposit(deposit);
            return RedirectToAction("Overview");
        }
        public IActionResult RecordDeposit(Deposit deposit)
        {
            if (Authentication.Type == "admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin");
            }
        }
        public IActionResult SaveNewDeposit(Deposit newDeposit)
        {
            repo.AddDeposit(newDeposit);
            
            return RedirectToAction("Overview");
        }
    }
}
