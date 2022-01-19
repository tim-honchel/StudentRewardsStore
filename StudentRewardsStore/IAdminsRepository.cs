using StudentRewardsStore.Models;

namespace StudentRewardsStore
{
    public interface IAdminsRepository
    {
        public void RegisterAdmin(Admin admin);
        public Admin LoginAdmin(string email);
        public void CheckPassword(string unhashed);
        public string encryption(string unhashed);

    }
}
