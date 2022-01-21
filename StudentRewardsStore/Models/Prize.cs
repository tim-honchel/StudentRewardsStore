﻿namespace StudentRewardsStore.Models
{
    public class Prize // the prize table in the database, with all of its columns as properties
    {
        public int PrizeID { get; set; } // primary key
        public string PrizeName { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
        public int Price { get; set; }
        public string DisplayStatus { get; set; }
        public int Inventory { get; set; }
        public int ValueTowardsLimit { get; set; }
        public int _OrganizationID { get; set; } // foreign key
    }
}