using Dapper;
using StudentRewardsStore.Models;
using System.Data;
using System;
using System.Collections.Generic;

namespace StudentRewardsStore
{
    public class DepositsRepository : IDepositsRepository
    {
        private readonly IDbConnection _conn;

        public IEnumerable<Deposit> ShowAllDeposits(int storeID)
        {
            return _conn.Query<Deposit>("SELECT * FROM deposits WHERE _Organization_ID = @OrganizationID;", new { OrganizationID = storeID });
        }
        public Deposit ViewDeposit(int depositID)
        {
            return _conn.QuerySingle<Deposit>("SELECT * FROM deposits WHERE DepositID = @DepositID;", new { DepositID = depositID });
        }
        public void UpdateDeposit(Deposit deposit)
        {
            _conn.Execute("UPDATE deposits SET Date = @Date, Amount = @Amount WHERE DepositID = @DepositID",
               new { Date = deposit.Date, Amount = deposit.Amount, DepositID = deposit.DepositID }); ;
        }
    }
}