using Dapper;
using StudentRewardsStore.Models;
using System.Data;
using System;
using System.Collections.Generic;

namespace StudentRewardsStore
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IDbConnection _conn;

        public OrdersRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        public IEnumerable<Order> ShowAllOrders(int storeID)
        {
            return _conn.Query<Order>("SELECT * FROM orders LEFT JOIN students ON orders._StudentID = students.StudentID LEFT JOIN prizes ON orders._PrizeID = prizes.PrizeID WHERE _Organization_ID_ = @OrganizationID;", new { OrganizationID = storeID });
        }
        public void UpdateOrder(Order order)
        {
            _conn.Execute("UPDATE orders SET OrderDate = @OrderDate, Cost = @Cost, Quantity = @Quantity, FulfillmentStatus = @FulfillmentStatus WHERE OrderID = @OrderID",
               new { OrderDate = order.OrderDate, Cost = order.Cost, Quantity = order.Quantity, FulfillmentStatus = order.FulfillmentStatus, OrderID = order.OrderID }); ;
        }
        public Order ViewOrder(int orderID)
        {
            return _conn.QuerySingle<Order>("SELECT * FROM orders LEFT JOIN students ON orders._StudentID = students.StudentID LEFT JOIN prizes ON orders._PrizeID = prizes.PrizeID WHERE OrderID = @OrderID;", new { OrderID = orderID });
        }
    }
}