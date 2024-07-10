using Microsoft.AspNetCore.Mvc;
using SalesOrderTest.Models;
using System.Diagnostics;

namespace SalesOrderTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult ExamplePage()
        {
            return View();
        }
    }
}