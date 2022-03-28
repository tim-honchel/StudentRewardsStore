namespace StudentRewardsStore.Models
{
    public class Admin // the admin table in the database, with all of its columns as properties
    {
        public int AdminID { get; set; } // primary key
        public string Email { get; set; }
        public string Unhashed { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
    }
}
