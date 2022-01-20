using Dapper;
using StudentRewardsStore.Models;
using System.Data;

namespace StudentRewardsStore
{
    public class OrganizationsRepository : IOrganizationsRepository
    {
        private readonly IDbConnection _conn;

        public OrganizationsRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        public Organization OpenStore(int id)
        {
            return _conn.QuerySingle<Organization>("SELECT * FROM organizations WHERE OrganizationID = @ID;", new { ID = id });
        }
        public void SaveNewStore(Organization newStore)
        {
            _conn.Execute("INSERT INTO organizations (OrganizationID, Name, StoreURL, CurrencyName, _AdminID) VALUES (@OrganizationID, @Name, @StoreURL, @CurrencyName, @AdminID);", new { OrganizationID = newStore.OrganizationID, Name = newStore.Name, StoreURL = newStore.StoreURL, CurrencyName = newStore.CurrencyName, AdminID = newStore._AdminID });
        }
        public Organization RefreshStore(Organization newStore)
        {
            return _conn.QuerySingle<Organization>("SELECT * FROM organizations WHERE Name = @Name AND StoreURL = @StoreURL;", new { Name = newStore.Name, StoreURL = newStore.StoreURL });
        }
    }
}