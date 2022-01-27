using System.Collections.Generic;

namespace StudentRewardsStore.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string PIN { get; set; }
        public string Category { get; set; }
        public int Balance { get; set; }
        public string Status { get; set; }
        public int _OrganizationID { get; set; } // foreign key
        public List<string> StatusDropdown { get; set; } // for display only
    }
}
