using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank.Data.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("actions") == null || HttpContext.Session.GetString("actions") == "guest")
            {
                return RedirectToRoute(new { controller = "Employee", action = "Login" });
            }
            else if (HttpContext.Session.GetString("actions") == "admin")
            {
                return RedirectToRoute(new { controller = "Employee", action = "AdminPage" });
            }
            else
            {
                return RedirectToRoute(new { controller = "Employee", action = "EmployeePage" });
            }
        }
    }
}
