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
            _conn.Execute("INSERT INTO organizations (OrganizationID, Name, CurrencyName, _AdminID) VALUES (@OrganizationID, @Name, @CurrencyName, @AdminID);", new { OrganizationID = newStore.OrganizationID, Name = newStore.Name, CurrencyName = newStore.CurrencyName, AdminID = newStore._AdminID });
        }
        public Organization RefreshStore(Organization newStore)
        {
            return _conn.QuerySingle<Organization>("SELECT * FROM organizations WHERE Name = @Name AND _AdminID = @AdminID;", new { Name = newStore.Name, AdminID = newStore._AdminID });
        }
        public void UpdateStore(Organization store)
        {
            _conn.Execute("UPDATE organizations SET Name = @Name, CurrencyName = @CurrencyName, StoreStatus = @StoreStatus, TimeZone = @TimeZone, CloseDay = @CloseDay, CloseTime = @CloseTime, OpenDay = @OpenDay, OpenTime = @OpenTime, WeeklyLimit = @WeeklyLimit WHERE OrganizationID = @OrganizationID;",
               new { Name = store.Name, CurrencyName = store.CurrencyName, StoreStatus = store.StoreStatus, TimeZone = store.TimeZone, CloseDay = store.CloseDay, CloseTime = store.CloseTime, OpenDay = store.OpenDay, OpenTime = store.OpenTime, WeeklyLimit = store.WeeklyLimit, OrganizationID = store.OrganizationID });

        }
    }
}

/*
 * public string AutoSchedule { get; set; }
        public string StoreStatus { get; set; }
        public string TimeZone { get; set; }
        public string CloseDay { get; set; }
        public int CloseTime { get; set; }
        public string OpenDay { get; set; }
        public int OpenTime { get; set; }
        public int WeeklyLimit { get; set; }*/