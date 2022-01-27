using System.Collections.Generic;

namespace StudentRewardsStore.Models

{
    public class Organization // the organization table in the database, with all of its columns as properties
    {
        public int OrganizationID { get; set; } // primary key
        public string Name { get; set; }
        public string CurrencyName { get; set; }
        public string AutoSchedule { get; set; }
        public string StoreStatus { get; set; }
        public string TimeZone { get; set; }
        public string CloseDay { get; set; }
        public int CloseTime { get; set; }
        public string OpenDay { get; set; }
        public int OpenTime { get; set; }
        public int WeeklyLimit { get; set; }
        public int _AdminID { get; set; } // foreign key
        public List<string> StatusDropdown { get; set; } // for display only


    }
}
