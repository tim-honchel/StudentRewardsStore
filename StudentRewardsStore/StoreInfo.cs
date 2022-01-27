using StudentRewardsStore.Models;
using System.Collections.Generic;

namespace StudentRewardsStore
{
    public static class StoreInfo
    {
        public static string StudentName { get; set; }
        public static int Balance { get; set; }
        public static string StudentStatus { get; set; }
        public static string StoreName { get; set; }
        public static string StoreStatus { get; set; }
        public static string Currency { get; set; }

        public static List<Prize> CurrentOrder = new List<Prize>();
        public static string CartMessage { get; set; }

    static StoreInfo()
        {

        }
       
    }
}
