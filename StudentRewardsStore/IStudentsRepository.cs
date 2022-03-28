using StudentRewardsStore.Models;
using System.Collections.Generic;

namespace StudentRewardsStore
{
    public interface IStudentsRepository
    {
        public IEnumerable<Student> ListStudents(int organizationID);
        public Student ViewStudent(int studentID);
        public void AddStudent(Student newStudent);
        public void UpdateStudent(Student student);
        public IEnumerable<Student> GetStudentIDs(int organizationID);
        public void LoadDemoStudent(Student student);

    }
}
