using Microsoft.AspNetCore.Mvc;

namespace StudentRewardsStore.Controllers
{
    public class DepositController : Controller
    {
        private readonly IDepositsRepository repo;
        public DepositController(IDepositsRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index(int depositID)
        {
            var deposit = repo.ViewDeposit(depositID); // retrieves the relevant deposit
            if (Authentication.StoreID == deposit._Organization_ID)
            {
                return View(deposit); // the specific deposit page
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
                var deposits = repo.ShowAllDeposits(Authentication.StoreID); // retrieves all the store's deposits
                return View(deposits); // the deposits overview page
            }
            else
            {
                return RedirectToAction("NotSignedIn", "Admin");
            }
        }
    }
}
