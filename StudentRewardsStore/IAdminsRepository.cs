using StudentRewardsStore.Models;
using System.Collections.Generic;

namespace StudentRewardsStore
{
    public interface IAdminsRepository
    {
        public void RegisterAdmin(Admin admin);
        public IEnumerable<Organization> ListStores(int adminID);
        public Admin CheckPassword(string email, string unhashed);
        public Admin GetAdminID(string email);
        public string encryption(string unhashed);

    }
}
