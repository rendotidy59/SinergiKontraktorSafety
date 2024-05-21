using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinergiKontraktorSafety.Data;
using System.Security.Claims;

namespace SinergiKontraktorSafety.Controllers
{

    [Authorize]
    public class DashboardController : Controller
    {
        private readonly SinergiDbContext sinergiDbContext;

        public DashboardController(SinergiDbContext sinergiDbContext )
        {
            this.sinergiDbContext = sinergiDbContext;
        }
        public IActionResult Index()
        {
            ViewBag.menu = "Dashboard";

            var successLogin = TempData["successLogin"] as string;
            ViewData["successLogin"] = successLogin;
            return View();
        }
    }
}
