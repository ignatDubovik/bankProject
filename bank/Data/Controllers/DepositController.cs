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
    public class DepositController : Controller
    {
        private readonly IDeposits _allDeposits;
        private readonly AppDBContent appDBContent;
        public DepositController(IDeposits ideposits, AppDBContent apd)
        {
            _allDeposits = ideposits;
            this.appDBContent = apd;
        }

        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                using (appDBContent)
                {
                    return View(appDBContent.Deposit.ToList());
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
                    return View(appDBContent.Deposit.Where(x => x.id == id).FirstOrDefault());
                }
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }

        public int finalAmountOfMoney(int initialAmount, int period, int capitalization, int percent)
        {
            double finalAmount = 0;
            finalAmount = 1 + (double)(percent) / (100 * (double)(capitalization));
            finalAmount = Math.Pow(finalAmount,(((double)period)*((double)capitalization)/12));
            finalAmount *= initialAmount;
            return (int)(finalAmount);
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
        public ActionResult Create(Deposit deposit)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                try
                {
                    using (appDBContent)
                    {
                        DepositType depType = new DepositType();
                        
                            depType = appDBContent.DepositType.Where(x => x.id == deposit.depositTypeID).FirstOrDefault();
                            if (depType != null)
                            {
                            if (deposit.initialMoney >= depType.minMoney && deposit.initialMoney <= depType.maxMoney)
                            {
                                deposit.plannedFinalAmountOfMoney = this.finalAmountOfMoney(deposit.initialMoney, depType.period, depType.capitalization, depType.percent);
                                appDBContent.Deposit.Add(deposit);
                                appDBContent.SaveChanges();
                            }
                            else
                            {
                                ViewBag.Message = "Сумма не подходит для данного типа вклада!";
                                return View();
                            }
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
                    return View(appDBContent.Deposit.Where(x => x.id == id).FirstOrDefault());
                }
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }
        [HttpPost]
        public ActionResult Edit(int id, Deposit deposit)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                try
                {
                    using (appDBContent)
                    {
                        deposit.id = id;
                        DepositType depType = new DepositType();
                        depType = appDBContent.DepositType.Where(x => x.id == deposit.depositTypeID).FirstOrDefault();

                        if (depType != null)
                        {
                            if (deposit.initialMoney >= depType.minMoney && deposit.initialMoney <= depType.maxMoney)
                            {
                                deposit.plannedFinalAmountOfMoney = this.finalAmountOfMoney(deposit.initialMoney, depType.period, depType.capitalization, depType.percent);
                                appDBContent.Entry(deposit).State = EntityState.Modified;
                                appDBContent.SaveChanges();
                            }
                            else
                            {
                                ViewBag.Message = "Сумма не подходит для данного типа вклада!";
                                return View();
                            }
                        }

                    }
                    return RedirectToAction("Index");
                }
                catch
                {
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
                    return View(appDBContent.Deposit.Where(x => x.id == id).FirstOrDefault());
                }
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }
        [HttpPost]
        public ActionResult Delete(int id, Deposit dp)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                try
                {
                    using (appDBContent)
                    {
                        Deposit deposit = appDBContent.Deposit.Where(x => x.id == id).FirstOrDefault();

                        appDBContent.Deposit.Remove(deposit);
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


        public ActionResult EmpViewsDeps()
        {
            if (!(HttpContext.Session.GetString("actions") == "admin" || HttpContext.Session.GetString("actions") == "guest"))
            {
                using (appDBContent)
                {
                    return View(appDBContent.Deposit.ToList());
                }
            }
            else
            {
                //return RedirectToAction("Employee/Login");
                return RedirectToRoute(new { controller = "Employee", action = "Login" });
            }
        }
        //EmpCreatesDep

        [HttpGet]
        public ActionResult EmpCreatesDep()
        {
            if (!(HttpContext.Session.GetString("actions") == "admin" || HttpContext.Session.GetString("actions") == "guest"))
            {
                return View();
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }
        [HttpPost]
        public ActionResult EmpCreatesDep(Deposit deposit)
        {
            if (!(HttpContext.Session.GetString("actions") == "admin" || HttpContext.Session.GetString("actions") == "guest"))
            {
                try
                {
                    using (appDBContent)
                    {
                        DepositType depType = new DepositType();

                        depType = appDBContent.DepositType.Where(x => x.id == deposit.depositTypeID).FirstOrDefault();
                        if (depType != null)
                        {
                            if (deposit.initialMoney >= depType.minMoney && deposit.initialMoney <= depType.maxMoney)
                            {
                                deposit.plannedFinalAmountOfMoney = this.finalAmountOfMoney(deposit.initialMoney, depType.period, depType.capitalization, depType.percent);
                                appDBContent.Deposit.Add(deposit);
                                appDBContent.SaveChanges();
                            }
                            else
                            {
                                ViewBag.Message = "Сумма не подходит для данного типа вклада!";
                                return View();
                            }
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
                    //return RedirectToAction("Index");
                }
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }


        public ActionResult TypeFilter()
        {
            List<Deposit> deposits = new List<Deposit>();
            deposits = appDBContent.Deposit.ToList();
            deposits = deposits.OrderBy(i => i.depositTypeID).ToList();
            return View("Index", deposits);
        }

        public ActionResult DateFilter()
        {
            List<Deposit> deposits = new List<Deposit>();
            deposits = appDBContent.Deposit.ToList();
            deposits = deposits.OrderBy(i => i.dateOfOpening).ToList();
            return View("Index", deposits);
        }


        public ActionResult TypeFilter1()
        {
            List<Deposit> deposits = new List<Deposit>();
            deposits = appDBContent.Deposit.ToList();
            deposits = deposits.OrderBy(i => i.depositTypeID).ToList();
            return View("EmpViewsDeps", deposits);
        }

        public ActionResult DateFilter1()
        {
            List<Deposit> deposits = new List<Deposit>();
            deposits = appDBContent.Deposit.ToList();
            deposits = deposits.OrderBy(i => i.dateOfOpening).ToList();
            return View("EmpViewsDeps", deposits);
        }


    }
}