using StudentRewardsStore.Models;

namespace StudentRewardsStore
{
    public interface IOrganizationsRepository
    {
        public void SaveNewStore(Organization organization);
    }
}
