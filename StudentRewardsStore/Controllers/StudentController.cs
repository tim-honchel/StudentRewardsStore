using Microsoft.AspNetCore.Mvc;
using System;
using StudentRewardsStore.Models;
using System.Collections.Generic;

namespace StudentRewardsStore.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentsRepository studentRepo;
        private readonly IAdminsRepository adminRepo;

        public StudentController(IStudentsRepository studentRepo, IAdminsRepository adminRepo)
        {
            this.studentRepo = studentRepo; // data repository for the student table
            this.adminRepo = adminRepo;
        }
        public IActionResult Index(int id) // passes in studentID from the student overview page
        {
            var student = studentRepo.ViewStudent(id); // retrieves the relevant student's data
            if (Authentication.StoreID == student._OrganizationID) // verifies that the admin is authorized to view this student's information
            {
                student.StatusDropdown = new List<string>() { "active", "inactive" }; // sets up a dropdown list for the student's status
                student.StatusDropdown.Remove(student.Status); // prevents duplicate values in the dropdown list
                return View(student); // specific student's page
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin"); // redirects if the admin is not authorized
            }
            
        }
        public IActionResult Overview()
        {
            if ((Authentication.Type == "admin" || Authentication.Type == "demo admin") && Authentication.StoreID > 0) // authenticates that an admin is signed in
            {
                var students = studentRepo.ListStudents(Authentication.StoreID); // retrieves the data of all students associated with the store
                return View(students); // student overview page
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin"); // redirects if an admin is not signed in
            }
        }
        public IActionResult AddStudent()
        {
            if ((Authentication.Type == "admin" || Authentication.Type == "demo admin") && Authentication.StoreID > 0) // authenticates that an admin is signed in
            {
                var student = new Student();
                student.StatusDropdown = new List<string>() { "active", "inactive" }; // sets up a dropdown list for the student's status
                Authentication.LastAction = DateTime.Now;
                adminRepo.LoginAdmin();
                return View(student); // add student page
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin");
            }
        }
        public IActionResult SaveNewStudent(Student newStudent) // passes in student data from the add student page
        {
            try
            {
                studentRepo.AddStudent(newStudent); // writes the new student into the database
                Authentication.LastAction = DateTime.Now;
                adminRepo.LoginAdmin();
                return RedirectToAction("Overview"); // student overview page
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home"); // redirects if there is an error writing to the database
            }
        }
        public IActionResult UpdateStudent(Student student) // passes in student data from the student index page
        {
            try
            {
                studentRepo.UpdateStudent(student); // updates the student in the database
                Authentication.LastAction = DateTime.Now;
                adminRepo.LoginAdmin();
                return RedirectToAction("Overview"); // student overview page
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home"); // redirects if there is an error writing to the database
            }
        }
    }
}
