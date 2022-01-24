using System;

namespace StudentRewardsStore.Models
 
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public int Cost { get; set; }
        public string FulfillmentStatus { get; set; }
        public int _StudentID { get; set; }
        public int _PrizeID { get; set; }
        public int _Organization_ID_ { get; set; }
    }
}
