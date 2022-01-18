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
        public void SaveNewStore(Organization newStore)
        {
            _conn.Execute("INSERT INTO organizations (ID, Name, StoreURL, CurrencyName, Email, Password) VALUES (@ID, @Name, @StoreURL, @CurrencyName, @Email, @Password);", new { ID = newStore.ID, Name = newStore.Name, StoreURL = newStore.StoreURL, CurrencyName = newStore.CurrencyName, Email = newStore.Email, Password = newStore.Password });
        }
    }
}