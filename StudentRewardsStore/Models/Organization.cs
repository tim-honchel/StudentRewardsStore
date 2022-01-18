namespace StudentRewardsStore.Models
{
    public class Organization
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string StoreURL { get; set; }
        public string CurrencyName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string StoreStatus { get; set; }
        public string TimeZone { get; set; }
        public string CloseDay { get; set; }
        public int CloseTime { get; set; }
        public string OpenDay { get; set; }
        public int OpenTime { get; set; }
        public int WeeklyLimit { get; set; }


    }
}
