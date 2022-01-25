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
        public void SaveNewOrders(IEnumerable<Order> newOrders)
        {
            foreach (Order order in newOrders)
            {
                _conn.Execute("INSERT INTO orders (OrderID, OrderDate, Quantity, Cost, FulfillmentStatus, _StudentID, _PrizeID, _Organization_ID_) VALUES (@OrderID, @OrderDate, @Quantity, @Cost, @FulfillmentStatus, @StudentID, @PrizeID, @OrganizationID);", new { OrderID = order.OrderID, OrderDate = order.OrderDate, Quantity = order.Quantity, Cost = order.Cost, FulfillmentStatus = order.FulfillmentStatus, StudentID = order._StudentID, PrizeID = order._PrizeID, OrganizationID = order._Organization_ID_ });
                var student = _conn.QuerySingle<Student>("SELECT * FROM students WHERE StudentID = @StudentID;", new { StudentID = order._StudentID });
                var newBalance = student.Balance - order.Cost;
                _conn.Execute("UPDATE students SET Balance = @Balance WHERE StudentID = @StudentID;", new {Balance = newBalance, StudentID = order._StudentID });
                var prize = _conn.QuerySingle<Prize>("SELECT * FROM prizes WHERE PrizeID = @PrizeID;", new { PrizeID = order._PrizeID });
                var newInventory = prize.Inventory - order.Quantity;
                _conn.Execute("UPDATE prizes SET Inventory = @Inventory WHERE PrizeID = @PrizeID", new { Inventory = newInventory, PrizeID = order._PrizeID });
            }
        }
    }
}