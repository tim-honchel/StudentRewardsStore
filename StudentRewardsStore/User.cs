namespace StudentRewardsStore
{
    public class User
    {
        public string Type { get; set; }
        public int? AdminId { get; set; }
        public int? StoreID { get; set; }
        public int? StudentID { get; set; }

        public User()
        {

        }
        public User (string type, int adminID, int storeID, int studentID)
        {
            Type = type;
            AdminId = adminID;
            StoreID = storeID;
            StudentID = studentID;
        }
    }
}
