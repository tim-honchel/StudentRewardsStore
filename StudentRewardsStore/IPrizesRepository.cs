using StudentRewardsStore.Models;
using System.Collections.Generic;

namespace StudentRewardsStore
{
    public interface IPrizesRepository
    {
        public IEnumerable<Prize> ListPrizes(int organizationID);
        public Prize ViewPrize(int prizeID);
        public void UpdatePrize(Prize prize);
        public void AddPrize(Prize newPrize);
        

    }
}
