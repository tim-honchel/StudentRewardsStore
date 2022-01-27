using System.Collections.Generic;

namespace StudentRewardsStore.Models

{
    public class Prize // the prize table in the database, with all of its columns as properties
    {
        public int PrizeID { get; set; } // primary key
        public string PrizeName { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public int Price { get; set; }
        public string DisplayStatus { get; set; }
        public int Inventory { get; set; }
        public int ValueTowardsLimit { get; set; }
        public int _OrganizationID { get; set; } // foreign key
        public int Quantity { get; set; } // for facilitating orders only
        public List<int> QuantitySelections { get; set; } // for facilitating orders only
        public int Cost { get; set; } // for facilitating orders only
        public List<int> PriceDropdown { get; set; } // for facilitating orders only
        public List<string> StatusDropdown { get; set; } // for facilitating orders only
        public List<int> InventoryDropdown { get; set; } // for facilitating orders only
    }
}
