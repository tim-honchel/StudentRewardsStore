using Microsoft.AspNetCore.Mvc;
using System;
using StudentRewardsStore.Models;
using System.Collections.Generic;

namespace StudentRewardsStore.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentsRepository repo;

        public StudentController(IStudentsRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index(int id)
        {
            var student = repo.ViewStudent(id);
            student.StatusDropdown = new List<string>() { "active", "inactive" };
            if (Authentication.StoreID == student._OrganizationID)
            {
                return View(student);
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
                var students = repo.ListStudents(Authentication.StoreID);
                return View(students);
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin");
            }
        }
        public IActionResult AddStudent()
        {
            if (Authentication.Type == "admin" && Authentication.StoreID > 0)
            {
                var student = new Student();
                student.StatusDropdown = new List<string>() { "active", "inactive" };
                return View(student);
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin");
            }
        }
        public IActionResult SaveNewStudent(Student newStudent)
        {
            try
            {
                repo.AddStudent(newStudent);
                return RedirectToAction("Overview");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home"); // redirects if there is an error writing to the database
            }
        }
        public IActionResult UpdateStudent(Student student)
        {
            try
            {
                repo.UpdateStudent(student);
                return RedirectToAction("Overview");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home"); // redirects if there is an error writing to the database
            }
        }
    }
}
