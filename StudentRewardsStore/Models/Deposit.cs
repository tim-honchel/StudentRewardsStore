using System;

namespace StudentRewardsStore.Models

{
    public class Deposit
    {
        public int DepositID { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public int _Student_ID { get; set; }
        public int _Organization_ID { get; set; }
    }
}
