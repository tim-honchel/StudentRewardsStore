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
            _conn = conn;
        }
        public IEnumerable<Student> ListStudents(int organizationID)
        {
            return _conn.Query<Student>("SELECT * FROM students WHERE _OrganizationID = @OrganizationID ORDER BY StudentName;", new { OrganizationID = organizationID });
        }
        public Student ViewStudent(int studentID)
        {
            return _conn.QuerySingle<Student>("SELECT * FROM students WHERE StudentID = @StudentID;", new { StudentID = studentID });
            
        }
        public void AddStudent(Student newStudent)
        {
            _conn.Execute("INSERT INTO students (StudentID, StudentName, PIN, Category, Balance, Status, _OrganizationID) VALUES (@StudentID, @StudentName, @PIN, @Category, @Balance, @Status, @OrganizationID);", new { StudentID = newStudent.StudentID, StudentName = newStudent.StudentName, PIN = newStudent.PIN, Category = newStudent.Category, Balance = newStudent.Balance, Status = newStudent.Status, OrganizationID = newStudent._OrganizationID });

        }
        public void UpdateStudent(Student student)
        {
            _conn.Execute("UPDATE students SET StudentName = @StudentName, PIN = @PIN, Category = @Category, Balance = @Balance, Status = @Status WHERE StudentID = @StudentID;", new { StudentName = student.StudentName, PIN = student.PIN, Category = student.Category, Balance = student.Balance, Status = student.Status, StudentID = student.StudentID });
        }
        public IEnumerable<Student> GetStudentIDs(int organizationID)
        {
            return _conn.Query<Student>("SELECT StudentID, StudentName FROM students WHERE _OrganizationID = @OrganizationID ORDER BY StudentName;", new { OrganizationID = organizationID });
        }
    }
}