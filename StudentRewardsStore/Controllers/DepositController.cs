using Microsoft.AspNetCore.Mvc;
using System;
using StudentRewardsStore.Models;
using System.Collections.Generic;

namespace StudentRewardsStore.Controllers
{
    public class DepositController : Controller
    {
        private readonly IDepositsRepository repo;
        private readonly IStudentsRepository studentRepo;
        public DepositController(IDepositsRepository repo, IStudentsRepository studentRepo)
        {
            this.repo = repo;
            this.studentRepo = studentRepo;
        }
        public IActionResult Index(int id)
        {
            var deposit = repo.ViewDeposit(id); // retrieves the relevant deposit
            if (Authentication.StoreID == deposit._Organization_ID)
            {
                deposit.AmountDropdown = new List<int>();
                for (int i = 0; i <= 100; i++)
                {
                    deposit.AmountDropdown.Add(i);
                }
                for (int i = -1; i>=-100; i--)
                {
                    deposit.AmountDropdown.Add(i);
                }
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
            try
            {
                repo.UpdateDeposit(deposit);
                return RedirectToAction("Overview");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home"); // redirects if there is an error writing to the database
            }
        }
        public IActionResult RecordDeposit()
        {
            if (Authentication.Type == "admin")
            {
                var studentsForDropdownList = studentRepo.GetStudentIDs(Authentication.StoreID);
                var deposit = new Deposit();
                deposit.StudentDropdown = studentsForDropdownList;
                deposit.AmountDropdown = new List<int>();
                for (int i = 0; i <= 100; i++)
                {
                    deposit.AmountDropdown.Add(i);
                }
                for (int i = -1; i >= -100; i--)
                {
                    deposit.AmountDropdown.Add(i);
                }
                return View(deposit);
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin");
            }
        }
        public IActionResult SaveNewDeposit(Deposit newDeposit)
        {
            var student = studentRepo.ViewStudent(newDeposit._Student_ID);
            if (student._OrganizationID == Authentication.StoreID) // validates the student is in the organization
            {
                try
                {
                    repo.AddDeposit(newDeposit);
                    return RedirectToAction("Overview");
                }
                catch (Exception)
                {
                    return RedirectToAction("Error", "Home"); // redirects if there is an error writing to the database
                }
            }
            else
            {
                return RedirectToAction("Error", "Home"); // redirects if the user attempts to deposit funds for a student who is not in their organization
            }
            
        }
    }
}
