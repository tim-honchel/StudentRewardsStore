using StudentRewardsStore.Models;
using System.Collections.Generic;
using System;

namespace StudentRewardsStore
{
    public interface IOrdersRepository
    {
        public IEnumerable<Order> ShowAllOrders(int storeID);
        public void UpdateOrder(Order order);
        public Order ViewOrder(int orderID);


    }
}
