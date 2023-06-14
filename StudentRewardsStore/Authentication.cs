using System;

namespace StudentRewardsStore
{
    public static class Authentication // global variables used to log the user in, store their information, and authorize their access to various pages
    {
        public static string Type { get; set; }
        public static int AdminID { get; set; }
        public static int StoreID { get; set; }
        public static int StudentID { get; set; }
        public static DateTime LastAction { get; set; }
        public static bool MultipleUsers { get; set; }

 

    }
}
