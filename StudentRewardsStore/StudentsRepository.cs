using Dapper;
using StudentRewardsStore.Models;
using System.Data;
using System.Text;
using XSystem.Security.Cryptography;
using System.Collections.Generic;
using System;

namespace StudentRewardsStore
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly IDbConnection _conn;

        public StudentsRepository(IDbConnection conn)
        {
            _conn = conn; // connection to the MySQL student table data via Dapper
        }
        public IEnumerable<Student> ListStudents(int organizationID) // passes in an organization's ID and returns a list of all students associated with that organization
        {
            return _conn.Query<Student>("SELECT * FROM students WHERE _OrganizationID = @OrganizationID ORDER BY StudentName;", new { OrganizationID = organizationID });
        }
        public Student ViewStudent(int studentID) // passes in a student's ID and returns all of that student's data
        {
            return _conn.QuerySingle<Student>("SELECT * FROM students WHERE StudentID = @StudentID;", new { StudentID = studentID });
            
        }
        public void AddStudent(Student newStudent) // passes in a new student's data and inserts it into to the database
        {
            _conn.Execute("INSERT INTO students (StudentID, StudentName, PIN, Category, Balance, Status, _OrganizationID) VALUES (@StudentID, @StudentName, @PIN, @Category, @Balance, @Status, @OrganizationID);", new { StudentID = newStudent.StudentID, StudentName = newStudent.StudentName, PIN = newStudent.PIN, Category = newStudent.Category, Balance = newStudent.Balance, Status = newStudent.Status, OrganizationID = newStudent._OrganizationID });

        }
        public void UpdateStudent(Student student) // passes in a student's data and updates it in the database
        {
            _conn.Execute("UPDATE students SET StudentName = @StudentName, PIN = @PIN, Category = @Category, Status = @Status WHERE StudentID = @StudentID;", new { StudentName = student.StudentName, PIN = student.PIN, Category = student.Category, Status = student.Status, StudentID = student.StudentID });
        }
        public IEnumerable<Student> GetStudentIDs(int organizationID) // passes in an organization's ID and gets a list of all students' IDs and names to be used in a dropdown list
        {
            return _conn.Query<Student>("SELECT StudentID, StudentName FROM students WHERE _OrganizationID = @OrganizationID ORDER BY StudentName;", new { OrganizationID = organizationID });
        }
        public void LoadDemoStudent(Student student)
        {
            _conn.Execute("UPDATE students SET StudentName = @StudentName, PIN = @PIN, Category = @Category, Status = @Status, Balance = @Balance WHERE StudentID = @StudentID;", new { StudentName = student.StudentName, PIN = student.PIN, Category = student.Category, Balance = student.Balance, Status = student.Status, StudentID = student.StudentID });
        }
    }
}