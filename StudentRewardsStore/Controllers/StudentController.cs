using Microsoft.AspNetCore.Mvc;
using System;
using StudentRewardsStore.Models;

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
                return View();
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin");
            }
        }
        public IActionResult SaveNewStudent(Student newStudent)
        {
            repo.AddStudent(newStudent);
            return RedirectToAction("Overview");
        }
        public IActionResult UpdateStudent(Student student)
        {
            repo.UpdateStudent(student);
            return RedirectToAction("Overview");
        }
    }
}
