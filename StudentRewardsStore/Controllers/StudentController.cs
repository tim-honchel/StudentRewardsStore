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
            return View(student);
        }
        public IActionResult Overview()
        {
            var authenticate = Convert.ToInt32(TempData["authenticateOrganizationID"]);
            ViewBag.Message = authenticate;
            var students = repo.ListStudents(authenticate);
            return View(students);
        }
        public IActionResult AddStudent()
        {
            var authenticate = Convert.ToInt32(TempData["authenticateOrganizationID"]);
            ViewBag.Message = authenticate;
            return View();
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
