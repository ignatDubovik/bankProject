
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bank.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bank.Data.Controllers
{
    public class CreditController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (!(HttpContext.Session.GetString("actions") == "admin" || HttpContext.Session.GetString("actions") == "guest"))
            {
                Credit credit = new Credit();
                credit.creditSum = 0;
                credit.creditPeriod = 0;
                credit.payout = 0;
                credit.percent = 0;
                credit.isDiff = false;
                return View(credit);
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }
        [HttpPost]
        public ActionResult Index(Credit credit)
        {
            if (!(HttpContext.Session.GetString("actions") == "admin" || HttpContext.Session.GetString("actions") == "guest"))
            {
                try
                {
                    if (credit.creditPeriod > 0 && credit.creditSum > 0 && credit.percent > 0)
                    {
                        if (credit.isDiff)
                        {
                            credit.payout = credit.payOutDiff();
                        }
                        else
                        {
                            credit.payout = credit.payOutAnnuit();
                        }
                        return View(credit);
                    }
                    else
                    {
                        ViewBag.Message = "Данные некорректны!";
                        return View();
                    }
                }
                catch
                {
                    return View();
                }
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }
    }
    }
