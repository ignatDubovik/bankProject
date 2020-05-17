using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bank.Data.Interfaces;
using bank.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bank.Data.Controllers
{
    public class DepositTypeController : Controller
    {
        private readonly IDepositTypes _allTypes;
        private readonly AppDBContent appDBContent;
        public DepositTypeController(IDepositTypes iTypes, AppDBContent apd)
        {
            _allTypes = iTypes;
            this.appDBContent = apd;
        }

        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                using (appDBContent)
                {
                    return View(appDBContent.DepositType.ToList());
                }
            }
            else
            {
                //return RedirectToAction("Employee/Login");
                return RedirectToRoute(new { controller = "Employee", action = "Login" });
            }
        }

        public ActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                using (appDBContent)
                {
                    return View(appDBContent.DepositType.Where(x => x.id == id).FirstOrDefault());
                }
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }


        [HttpGet]
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                return View();
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }
        [HttpPost]
        public ActionResult Create(DepositType depositType)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                try
                {
                    using (appDBContent)
                    {
                        if (depositType.capitalization > 0 && depositType.minMoney > 0 && depositType.percent > 0 && depositType.percent < 100 && depositType.period > 0 && depositType.maxMoney > depositType.minMoney)
                        {
                            appDBContent.DepositType.Add(depositType);
                            appDBContent.SaveChanges();
                        }
                        else
                        {
                            ViewBag.Message = "Данные некорректны!";
                            return View();
                        }
                    }
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                    //return RedirectToAction("Index");
                }
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                using (appDBContent)
                {
                    return View(appDBContent.DepositType.Where(x => x.id == id).FirstOrDefault());
                }
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }
        [HttpPost]
        public ActionResult Edit(int id, DepositType depositType)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                try
                {
                    using (appDBContent)
                    {

                        if (depositType.capitalization > 0 && depositType.minMoney > 0 && depositType.percent > 0 && depositType.percent < 100 && depositType.period > 0 && depositType.maxMoney > depositType.minMoney)
                        {
                            depositType.id = id;
                            appDBContent.Entry(depositType).State = EntityState.Modified;
                            appDBContent.SaveChanges();
                        }
                        else
                        {
                            ViewBag.Message = "Данные некорректны!";
                            return View();
                        }
                    }
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "Данные некорректны!";
                    return View();
                }
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                using (appDBContent)
                {
                    return View(appDBContent.DepositType.Where(x => x.id == id).FirstOrDefault());
                }
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }
        [HttpPost]
        public ActionResult Delete(int id, DepositType dt)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                try
                {
                    using (appDBContent)
                    {
                        DepositType dept = appDBContent.DepositType.Where(x => x.id == id).FirstOrDefault();

                        appDBContent.DepositType.Remove(dept);
                        appDBContent.SaveChanges();

                    }
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View("Delete");
                }
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }


        public ActionResult EmployeeDepTypeView()
        {
            if (!(HttpContext.Session.GetString("actions") == "admin" || HttpContext.Session.GetString("actions") == "guest"))
            {
                using (appDBContent)
                {
                    return View(appDBContent.DepositType.ToList());
                }
            }
            else
            {
                //return RedirectToAction("Employee/Login");
                return RedirectToRoute(new { controller = "Employee", action = "Login" });
            }
        }


        [HttpGet]
        public ActionResult DepositForecast()
        {
            if (!(HttpContext.Session.GetString("actions") == "admin" || HttpContext.Session.GetString("actions") == "guest"))
            {
                DepositType dep = new DepositType();
                dep.minMoney = 0; dep.maxMoney = 0; dep.percent = 0; dep.capitalization = 0; dep.period = 0;
                return View(dep);
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }
        [HttpPost]
        public ActionResult DepositForecast(DepositType depositType)
        {
            if (!(HttpContext.Session.GetString("actions") == "admin" || HttpContext.Session.GetString("actions") == "guest"))
            {
                try
                {
                    if (depositType.capitalization > 0 && depositType.minMoney > 0 && depositType.percent > 0 && depositType.percent < 100 && depositType.period > 0)
                    {
                        return View(depositType);
                    }
                    else
                    {
                        ViewBag.Message = "Данные некорректны!";
                        return View(depositType);
                    }
                    
                }
                catch
                {
                    ViewBag.Message = "Данные некорректны!";
                    return View();
                    //return RedirectToAction("Index");
                }
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }



        [HttpGet]
        public ActionResult DepForWOMin()
        {
            if (!(HttpContext.Session.GetString("actions") == "admin" || HttpContext.Session.GetString("actions") == "guest"))
            {
                DepositType dep = new DepositType();
                dep.minMoney = 0; dep.maxMoney = 0; dep.percent = 0; dep.capitalization = 0; dep.period = 0;
                return View(dep);
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }
        [HttpPost]
        public ActionResult DepForWOMin(DepositType depositType)
        {
            if (!(HttpContext.Session.GetString("actions") == "admin" || HttpContext.Session.GetString("actions") == "guest"))
            {
                try
                {
                    if (depositType.capitalization > 0 && depositType.maxMoney > 0 && depositType.percent > 0 && depositType.percent < 100 && depositType.period > 0)
                    {
                        double minMoney = (double)depositType.maxMoney;
                        //minMoney = minMoney / (Math.Pow((1+((double)depositType.percent)/(100*((double)depositType.capitalization))),((double)depositType.period)*((double)(depositType.capitalization))/12));
                        double koef = 1 + ((double)depositType.percent) / (((double)depositType.capitalization) * 100);
                        double power = ((double)depositType.period) * ((double)depositType.capitalization) / 12;
                        koef = Math.Pow(koef, power);
                        minMoney = minMoney / koef;
                        depositType.minMoney = (int)minMoney;
                        return View(depositType);
                    }
                    else
                    {
                        ViewBag.Message = "Данные некорректны!";
                        return View(depositType);
                    }
                    
                }
                catch
                {
                    ViewBag.Message = "Данные некорректны!";
                    return View();
                    //return RedirectToAction("Index");
                }
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }


        public ActionResult NameFilter()
        {
            List<DepositType> depositTypes = new List<DepositType>();
            depositTypes = appDBContent.DepositType.ToList();
            depositTypes = depositTypes.OrderBy(i => i.typeName).ToList();
            return View("Index", depositTypes);
        }

        public ActionResult PercentFilter()
        {
            List<DepositType> depositTypes = new List<DepositType>();
            depositTypes = appDBContent.DepositType.ToList();
            depositTypes = depositTypes.OrderByDescending(i => i.percent).ToList();
            return View("Index", depositTypes);
        }


        public ActionResult PeriodFilter()
        {
            List<DepositType> depositTypes = new List<DepositType>();
            depositTypes = appDBContent.DepositType.ToList();
            depositTypes = depositTypes.OrderBy(i => i.period).ToList();
            return View("Index", depositTypes);
        }


        public ActionResult NameFilter1()
        {
            List<DepositType> depositTypes = new List<DepositType>();
            depositTypes = appDBContent.DepositType.ToList();
            depositTypes = depositTypes.OrderBy(i => i.typeName).ToList();
            return View("EmployeeDepTypeView", depositTypes);
        }
        public ActionResult PercentFilter1()
        {
            List<DepositType> depositTypes = new List<DepositType>();
            depositTypes = appDBContent.DepositType.ToList();
            depositTypes = depositTypes.OrderByDescending(i => i.percent).ToList();
            return View("EmployeeDepTypeView", depositTypes);
        }


        public ActionResult PeriodFilter1()
        {
            List<DepositType> depositTypes = new List<DepositType>();
            depositTypes = appDBContent.DepositType.ToList();
            depositTypes = depositTypes.OrderBy(i => i.period).ToList();
            return View("EmployeeDepTypeView", depositTypes);
        }





    }
}