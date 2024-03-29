﻿using Microsoft.AspNetCore.Mvc;
using StudentRewardsStore.Models;
using System;
using System.Collections.Generic;

namespace StudentRewardsStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrdersRepository orderRepo;
        private readonly IAdminsRepository adminRepo;
        public OrderController(IOrdersRepository orderRepo, IAdminsRepository adminRepo)
        {
            this.orderRepo = orderRepo;
            this.adminRepo = adminRepo;
        }
        public IActionResult Index(int id)
        {
            var order = orderRepo.ViewOrder(id); // retrieves the relevant order
            if (Authentication.StoreID == order._Organization_ID_)
            {
                order.StatusDropdown = new List<string>() { "unfulfilled", "fulfilled", "canceled" };
                order.StatusDropdown.Remove(order.FulfillmentStatus);
                order.CostDropdown = new List<int>();
                for (int i = 0; i <= 1000; i++)
                {
                    order.CostDropdown.Add(i);
                }
                order.CostDropdown.Remove(order.Cost);
                order.QuantityDropdown = new List<int>();
                for (int i = 0; i <= 50; i++)
                {
                    order.QuantityDropdown.Add(i);
                }
                order.QuantityDropdown.Remove(order.Quantity);
                return View(order); // the specific order page
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin");
            }
        }
        public IActionResult Overview()
        {
            if ((Authentication.Type == "admin" || Authentication.Type == "demo admin") && Authentication.StoreID > 0)
            {
                var orders = orderRepo.ShowAllOrders(Authentication.StoreID); // retrieves all the store's orders
                return View(orders); // the orders overview page
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin");
            }
        }
        public IActionResult UpdateOrder(Order order)
        {
            try
            {
                orderRepo.UpdateOrder(order);
                Authentication.LastAction = DateTime.Now;
                adminRepo.LoginAdmin();
                return RedirectToAction("Overview");
            }
            catch (System.Exception)
            {
                return RedirectToAction("Error", "Home"); // redirects if there is an error writing to the database
            }
        }
    }
}
