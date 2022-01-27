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
            return _conn.Query<Prize>("SELECT * FROM prizes LEFT JOIN organizations ON prizes._OrganizationID = organizations.OrganizationID WHERE prizes._OrganizationID = @OrganizationID ORDER BY prizes.Price;", new { OrganizationID = organizationID });
        }
        public Prize ViewPrize(int prizeID)
        {
            return _conn.QuerySingle<Prize>("SELECT * FROM prizes WHERE PrizeID = @PrizeID;", new { PrizeID = prizeID });
        }
        public void UpdatePrize(Prize prize)
        {
            _conn.Execute("UPDATE prizes SET PrizeName = @PrizeName, Description = @Description, ImageLink = @ImageLink, ImageWidth = @ImageWidth, ImageHeight = @ImageHeight, Price = @Price, DisplayStatus = @DisplayStatus, Inventory = @Inventory WHERE PrizeID = @PrizeID",
               new { PrizeName = prize.PrizeName, Description = prize.Description, ImageLink = prize.ImageLink, ImageWidth = prize.ImageWidth, ImageHeight = prize.ImageHeight, Price = prize.Price, DisplayStatus = prize.DisplayStatus, Inventory = prize.Inventory, PrizeID = prize.PrizeID }); ;

        }
        public void AddPrize(Prize newPrize)
        {
            _conn.Execute("INSERT INTO prizes (PrizeID, PrizeName, Description, ImageLink, ImageWidth, ImageHeight, Price, DisplayStatus, Inventory, _OrganizationID) VALUES (@PrizeID, @PrizeName, @Description, @ImageLink, @ImageWidth, @ImageHeight, @Price, @DisplayStatus, @Inventory, @OrganizationID);", new { PrizeID = newPrize.PrizeID, PrizeName = newPrize.PrizeName, Description = newPrize.Description, ImageLink = newPrize.ImageLink, ImageWidth = newPrize.ImageWidth, ImageHeight = newPrize.ImageHeight, Price = newPrize.Price, DisplayStatus = newPrize.DisplayStatus, Inventory = newPrize.Inventory, OrganizationID = newPrize._OrganizationID }) ;
            
        }
        public IEnumerable<Prize> ShowAvailablePrizes(int organizationID)
        {
            return _conn.Query<Prize>("SELECT * FROM prizes WHERE _OrganizationID = @OrganizationID AND DisplayStatus = 'show' ORDER BY Price;", new { OrganizationID = organizationID });
        }
    }
}