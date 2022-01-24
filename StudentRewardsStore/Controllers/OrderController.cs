using Microsoft.AspNetCore.Mvc;

namespace StudentRewardsStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrdersRepository repo;
        public OrderController(IOrdersRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index(int orderID)
        {
            var order = repo.ViewOrder(orderID); // retrieves the relevant order
            if (Authentication.StoreID == order._Organization_ID_)
            {
                return View(order); // the specific prize page
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin");
            }
        }
        public IActionResult Overview()
        {
            if (Authentication.Type == "admin" && Authentication.StoreID > 0)
            {
                var orders = repo.ShowAllOrders(Authentication.StoreID); // retrieves all the store's orders
                return View(orders); // the orders overview page
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin");
            }
        }
    }
}
