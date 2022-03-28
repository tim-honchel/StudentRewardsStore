using StudentRewardsStore.Models;

namespace StudentRewardsStore
{
    public interface IOrganizationsRepository
    {
        public Organization OpenStore(int id);
        public void SaveNewStore(Organization organization);
        public Organization RefreshStore(Organization newStore);
        public void UpdateStore(Organization store);
        public void LoadDemoStore(Organization store);
    }
}
