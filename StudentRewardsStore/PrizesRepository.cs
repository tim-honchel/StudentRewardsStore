using Dapper;
using StudentRewardsStore.Models;
using System.Data;
using System.Collections.Generic;

namespace StudentRewardsStore
{
    public class PrizesRepository : IPrizesRepository
    {
        private readonly IDbConnection _conn;

        public PrizesRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        public IEnumerable<Prize> ListPrizes(int organizationID)
        {
            return _conn.Query<Prize>("SELECT * FROM prizes LEFT JOIN organizations ON prizes._OrganizationID = organizations.OrganizationID WHERE prizes._OrganizationID = @OrganizationID;", new { OrganizationID = organizationID });
        }
        public Prize ViewPrize(int prizeID)
        {
            return _conn.QuerySingle<Prize>("SELECT * FROM prizes WHERE PrizeID = @PrizeID;", new { PrizeID = prizeID });
        }
        public void UpdatePrize(Prize prize)
        {
            _conn.Execute("UPDATE prizes SET PrizeName = @PrizeName, Description = @Description, ImageLink = @ImageLink, Price = @Price, DisplayStatus = @DisplayStatus, Inventory = @Inventory, ValueTowardsLimit = @ValueTowardsLimit  WHERE PrizeID = @PrizeID",
               new { PrizeName = prize.PrizeName, Description = prize.Description, ImageLink = prize.ImageLink, Price = prize.Price, DisplayStatus = prize.DisplayStatus, Inventory = prize.Inventory, ValueTowardsLimit = prize.ValueTowardsLimit, PrizeID = prize.PrizeID });

        }
        public void AddPrize(Prize newPrize)
        {
            _conn.Execute("INSERT INTO prizes (PrizeID, PrizeName, Description, ImageLink, Price, DisplayStatus, Inventory, ValueTowardsLimit, _OrganizationID) VALUES (@PrizeID, @PrizeName, @Description, @ImageLink, @Price, @DisplayStatus, @Inventory, @ValueTowardsLimit, @OrganizationID);", new { PrizeID = newPrize.PrizeID, PrizeName = newPrize.PrizeName, Description = newPrize.Description, ImageLink = newPrize.ImageLink, Price = newPrize.Price, DisplayStatus = newPrize.DisplayStatus, Inventory = newPrize.Inventory, ValueTowardsLimit = newPrize.ValueTowardsLimit, OrganizationID = newPrize._OrganizationID }) ;
            //_conn.Execute("INSERT INTO organizations (OrganizationID, Name, StoreURL, CurrencyName, _AdminID) VALUES (@OrganizationID, @Name, @StoreURL, @CurrencyName, @AdminID);", new { OrganizationID = newStore.OrganizationID, Name = newStore.Name, StoreURL = newStore.StoreURL, CurrencyName = newStore.CurrencyName, AdminID = newStore._AdminID });
        }
    }
}