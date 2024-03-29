﻿using System;
using System.Collections.Generic;

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
        public string StudentName { get; set; } // for display only
        public string PrizeName { get; set; } // for display only
        public int Price { get; set; } // for display only

        public List<int> QuantityDropdown { get; set; } // for display only
        public List<int> CostDropdown { get; set; }
        public List<string> StatusDropdown { get; set; }
    }
}
