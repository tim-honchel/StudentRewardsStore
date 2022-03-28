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
        public void LoadDemoPrizes()
        {
            _conn.Execute("UPDATE prizes SET PrizeName = 'Skittles', Description = '', ImageLink = 'https://www.skittles.com/sites/g/files/fnmzdf586/files/migrate-product-files/bam8afcev37jvz2mfpnk.png', ImageWidth = 150, ImageHeight = 150, Price = 2, DisplayStatus = 'show', Inventory = 1 WHERE PrizeID = 1");
            _conn.Execute("UPDATE prizes SET PrizeName = 'Starburst', Description = '', ImageLink = 'https://imagesvc.meredithcorp.io/v3/mm/image?url=https%3A%2F%2Fimg1.cookinglight.timeinc.net%2Fsites%2Fdefault%2Ffiles%2Fstyles%2F4_3_horizontal_-_1200x900%2Fpublic%2Fupdated_main_images%2F1110p42-starburst-funsize-x.jpg%3Fitok%3DKVA1qr8O', ImageWidth = 150, ImageHeight = 150, Price = 2, DisplayStatus = 'show', Inventory = 15 WHERE PrizeID = 3");
            _conn.Execute("UPDATE prizes SET PrizeName = 'Rolos', Description = '', ImageLink = 'https://images.heb.com/is/image/HEBGrocery/000112562', ImageWidth = 150, ImageHeight = 150, Price = 2, DisplayStatus = 'hide', Inventory = 10 WHERE PrizeID = 4");
            _conn.Execute("UPDATE prizes SET PrizeName = 'Tablet', Description = '', ImageLink = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSWFE3xWDbcRQdS4L8ZzfNW0Duyx7uVNdmG-nBBOzwm09YY6ILeWKqppYtgMTYNp9niKkE&usqp=CAU', ImageWidth = 150, ImageHeight = 150, Price = 200, DisplayStatus = 'hide', Inventory = 0 WHERE PrizeID = 5");
            _conn.Execute("UPDATE prizes SET PrizeName = 'Hot Wheels car', Description = '', ImageLink = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRzlhhUQZMMiTX_hYruxP88m_PnYTR-3HsB4g&usqp=CAU', ImageWidth = 150, ImageHeight = 150, Price = 10, DisplayStatus = 'show', Inventory = 33 WHERE PrizeID = 6");
            _conn.Execute("UPDATE prizes SET PrizeName = 'Smarties', Description = '', ImageLink = 'https://economycandy.com/wp-content/uploads/2022/01/2eaba100-4c83-4e03-bfca-8b1d989d8fc6.jpeg', ImageWidth = 150, ImageHeight = 150, Price = 2, DisplayStatus = 'show', Inventory = 24 WHERE PrizeID = 7");
            _conn.Execute("UPDATE prizes SET PrizeName = 'M&Ms', Description = '', ImageLink = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR1rRCRAsopNb5p-paY_fdt-3fLQIFarcTNKm_G_nNX8M5yiuZHD2c__MAXz3Wy8tEz_s0&usqp=CAU', ImageWidth = 150, ImageHeight = 150, Price = 2, DisplayStatus = 'show', Inventory = 17 WHERE PrizeID = 8");
            _conn.Execute("UPDATE prizes SET PrizeName = 'Hershey bar', Description = '', ImageLink = 'https://m.media-amazon.com/images/I/41RZnZZdP3L.jpg', ImageWidth = 150, ImageHeight = 150, Price = 2, DisplayStatus = 'show', Inventory = 22 WHERE PrizeID = 9");
            _conn.Execute("UPDATE prizes SET PrizeName = 'Lays Potato Chips', Description = '', ImageLink = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTuMEb4yWheURLLBbNn4jjcj3awBW1ok5DHJs5TN-JdntIXGp4I8V9vmTuPR3cfwF6XOe8&usqp=CAU', ImageWidth = 150, ImageHeight = 150, Price = 5, DisplayStatus = 'show', Inventory = 12 WHERE PrizeID = 11");
            _conn.Execute("UPDATE prizes SET PrizeName = 'Popcorn', Description = '', ImageLink = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSVlDBufG6e7_AJz-mcQmAH9BjBILME0AYm1g&usqp=CAU', ImageWidth = 150, ImageHeight = 150, Price = 10, DisplayStatus = 'show', Inventory = 19 WHERE PrizeID = 12");
            _conn.Execute("UPDATE prizes SET PrizeName = 'Puffer Ball', Description = '', ImageLink = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQkuwUT-2jwEWZ97x908RCzCXxXsx_8NNO4_Q&usqp=CAU', ImageWidth = 150, ImageHeight = 150, Price = 10, DisplayStatus = 'show', Inventory = 4 WHERE PrizeID = 13");
            _conn.Execute("UPDATE prizes SET PrizeName = 'Nail Polish', Description = '', ImageLink = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS5Uroi-yOaGTaXgyoX6s0ZLjxcQGWVmx0CPQ&usqp=CAU', ImageWidth = 150, ImageHeight = 150, Price = 15, DisplayStatus = 'show', Inventory = 3 WHERE PrizeID = 14");
            _conn.Execute("UPDATE prizes SET PrizeName = 'LOL Surprise Doll', Description = '', ImageLink = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT4oBdYQwEPb1QlRHPFVpF0jqpMZIcrVpvsWQ&usqp=CAU', ImageWidth = 150, ImageHeight = 150, Price = 25, DisplayStatus = 'show', Inventory = 2 WHERE PrizeID = 15");
            _conn.Execute("UPDATE prizes SET PrizeName = 'Pop It', Description = '', ImageLink = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTrL6k3bsjfyV-MLzXGdeZ3cwRaKqRmDfa4uw&usqp=CAU', ImageWidth = 150, ImageHeight = 150, Price = 30, DisplayStatus = 'show', Inventory = 4 WHERE PrizeID = 16");
            _conn.Execute("UPDATE prizes SET PrizeName = 'Takis', Description = '', ImageLink = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTD-tJr9g27sENVhBMYyQsWe6rA9lnrKAFQnw&usqp=CAU', ImageWidth = 150, ImageHeight = 150, Price = 10, DisplayStatus = 'show', Inventory = 0 WHERE PrizeID = 17");
            _conn.Execute("UPDATE prizes SET PrizeName = 'Basketball', Description = '', ImageLink = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSk1rWpPSYITYYiE6kf_VWmkw08qHwo5hJHhg&usqp=CAU', ImageWidth = 150, ImageHeight = 150, Price = 40, DisplayStatus = 'show', Inventory = 3 WHERE PrizeID = 18");
            _conn.Execute("UPDATE prizes SET PrizeName = 'Backpack with School Supplies', Description = '', ImageLink = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQo_RIqs1i7OpD05kU4W0tjpHG3FhrTOSNDLw&usqp=CAU', ImageWidth = 150, ImageHeight = 150, Price = 60, DisplayStatus = 'show', Inventory = 4 WHERE PrizeID = 19");
            _conn.Execute("UPDATE prizes SET PrizeName = 'Remote Control Car', Description = '', ImageLink = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSmZ_mGMH3wG1Q5tvKtMOVT0UR7F2Niduyxkw&usqp=CAU', ImageWidth = 150, ImageHeight = 150, Price = 80, DisplayStatus = 'show', Inventory = 2 WHERE PrizeID = 20");
            _conn.Execute("UPDATE prizes SET PrizeName = 'Bicycle', Description = '', ImageLink = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTixbsHQW__YNXNTisJuECjVhKFdEtiFaiapA&usqp=CAU', ImageWidth = 150, ImageHeight = 150, Price = 150, DisplayStatus = 'show', Inventory = 2 WHERE PrizeID = 21");


        }
    }
}