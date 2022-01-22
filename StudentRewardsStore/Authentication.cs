namespace StudentRewardsStore
{
    public static class Authentication
    {
        public static string Type { get; set; }
        public static int AdminID { get; set; }
        public static int StoreID { get; set; }
        public static int StudentID { get; set; }

        static Authentication()
        {

        }
        /*static User(string type, int adminID, int storeID, int studentID)
        {
            Type = type;
            AdminId = adminID;
            StoreID = storeID;
            StudentID = studentID;
        }*/
    }
}
