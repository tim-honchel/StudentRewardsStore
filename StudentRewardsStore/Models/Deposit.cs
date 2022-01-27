using System;
using System.Collections.Generic;

namespace StudentRewardsStore.Models

{
    public class Deposit
    {
        public int DepositID { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public int _Student_ID { get; set; }
        public int _Organization_ID { get; set; }
        public string StudentName { get; set; } // for display only
        public IEnumerable<Student> StudentDropdown { get; set; } // for display only
        public List<int> AmountDropdown { get; set; } // for display only
        
    }
}
