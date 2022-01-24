using StudentRewardsStore.Models;
using System.Collections.Generic;
using System;

namespace StudentRewardsStore
{
    public interface IDepositsRepository
    {
        public IEnumerable<Deposit> ShowAllDeposits(int storeID);
        public Deposit ViewDeposit(int depositID);
        public void UpdateDeposit(Deposit deposit);
        public void AddDeposit(Deposit newDeposit);
        



    }
}
