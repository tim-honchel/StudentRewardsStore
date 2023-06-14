using Microsoft.AspNetCore.Mvc;
using System;
using StudentRewardsStore.Models;
using System.Collections.Generic;

namespace StudentRewardsStore.Controllers
{
    public class DepositController : Controller
    {
        private readonly IDepositsRepository depositRepo;
        private readonly IStudentsRepository studentRepo;
        private readonly IAdminsRepository adminRepo;
        public DepositController(IDepositsRepository depositRepo, IStudentsRepository studentRepo, IAdminsRepository adminRepo)
        {
            this.depositRepo = depositRepo; // deposit data repository
            this.studentRepo = studentRepo; // student data repository
            this.adminRepo = adminRepo;
        }
        public IActionResult Index(int id)
        {
            var deposit = depositRepo.ViewDeposit(id); // retrieves the relevant deposit
            if (Authentication.StoreID == deposit._Organization_ID) // verifies the user is authorized to access this page
            {
                deposit.AmountDropdown = new List<int>(); // sets up a dropdown list for the deposit amount
                for (int i = 0; i <= 100; i++) // populates the dropdown menu with values 0 to 100
                {
                    deposit.AmountDropdown.Add(i);
                }
                for (int i = -1; i>=-100; i--) // also populates the menu with values -1 to -100
                {
                    deposit.AmountDropdown.Add(i);
                }
                deposit.AmountDropdown.Remove(deposit.Amount);
                return View(deposit); // the specific deposit page
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin"); // redirects if authorization failed
            }
        }
        public IActionResult Overview()
        {
            if ((Authentication.Type == "admin" || Authentication.Type == "demo admin") && Authentication.StoreID > 0) // authenticates a user is logged in
            {
                var deposits = depositRepo.ShowAllDeposits(Authentication.StoreID); // retrieves all the store's deposits
                return View(deposits); // the deposits overview page
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin"); // redirects if no user is logged in
            }
        }
        public IActionResult UpdateDeposit(Deposit deposit)
        {
            try
            {
                depositRepo.UpdateDeposit(deposit); // writes the updated values to the database
                Authentication.LastAction = DateTime.Now;
                adminRepo.LoginAdmin();
                return RedirectToAction("Overview"); // the deposits overview page
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home"); // redirects if there is an error writing to the database
            }
        }
        public IActionResult RecordDeposit()
        {
            if (Authentication.Type == "admin" || Authentication.Type == "demo admin") //authenticates a user is logged in
            {
                var studentsForDropdownList = studentRepo.GetStudentIDs(Authentication.StoreID);
                var deposit = new Deposit(); // sets up dropdown lists for deposit amount and the student making the deposit
                deposit.StudentDropdown = studentsForDropdownList;  // populates the student dropdown list with all the organization's student IDs and names
                deposit.AmountDropdown = new List<int>();
                for (int i = 0; i <= 100; i++) // // populates the amount dropdown with values from 0 to 100
                {
                    deposit.AmountDropdown.Add(i);
                }
                for (int i = -1; i >= -100; i--) // also populates the amount dropdown with values from -1 to -100
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
                    depositRepo.AddDeposit(newDeposit);
                    Authentication.LastAction = DateTime.Now;
                    adminRepo.LoginAdmin();
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
