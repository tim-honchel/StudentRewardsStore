namespace StudentRewardsStore.Models
{
    public class Admin
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Unhashed { get; set; }
        public string Password { get; set; }
    }
}
