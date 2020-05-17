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
    public class ContractController : Controller
    {

        private readonly IContracts _allContracts;
        private readonly AppDBContent appDBContent;
        public ContractController(IContracts icontracts, AppDBContent apd)
        {
            _allContracts = icontracts;
            this.appDBContent = apd;
        }

        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                using (appDBContent)
                {
                    return View(appDBContent.Contract.ToList());
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
                    return View(appDBContent.Contract.Where(x => x.id == id).FirstOrDefault());
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
        public ActionResult Create(Contract contract)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                try
                {
                    using (appDBContent)
                    {
                        appDBContent.Contract.Add(contract);
                        appDBContent.SaveChanges();
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
                    return View(appDBContent.Contract.Where(x => x.id == id).FirstOrDefault());
                }
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }
        [HttpPost]
        public ActionResult Edit(int id, Contract contract)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                try
                {
                    using (appDBContent)
                    {
                        contract.id = id;
                        appDBContent.Entry(contract).State = EntityState.Modified;
                        appDBContent.SaveChanges();
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
                    return View(appDBContent.Contract.Where(x => x.id == id).FirstOrDefault());
                }
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }
        [HttpPost]
        public ActionResult Delete(int id, Contract co)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                try
                {
                    using (appDBContent)
                    {
                        Contract contract = appDBContent.Contract.Where(x => x.id == id).FirstOrDefault();

                        appDBContent.Contract.Remove(contract);
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


        public ActionResult EmployeeViewsContract()
        {
            if (!(HttpContext.Session.GetString("actions") == "admin" || HttpContext.Session.GetString("actions") == "guest"))
            {
                int EmployeeID = int.Parse(HttpContext.Session.GetString("actions"));
                using (appDBContent)
                {
                    return View(appDBContent.Contract.Where(x => x.employeeID == EmployeeID).ToList());
                }
            }
            else
            {
                //return RedirectToAction("Employee/Login");
                return RedirectToRoute(new { controller = "Employee", action = "Login" });
            }
        }


        [HttpGet]
        public ActionResult CreateContractEmployee()
        {
            if (!(HttpContext.Session.GetString("actions") == "admin" || HttpContext.Session.GetString("actions") == "guest"))
            {
                return View();
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }
        [HttpPost]
        public ActionResult CreateContractEmployee(Contract contract)
        {
            if (!(HttpContext.Session.GetString("actions") == "admin" || HttpContext.Session.GetString("actions") == "guest"))
            {
                try
                {
                    int EmployeeID = int.Parse(HttpContext.Session.GetString("actions"));
                    using (appDBContent)
                    {
                        contract.employeeID = EmployeeID;
                        appDBContent.Contract.Add(contract);
                        appDBContent.SaveChanges();
                    }
                    return RedirectToAction("EmployeeViewsContract");
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



        public ActionResult ClientFilter()
        {
            List<Contract> contracts = new List<Contract>();
            contracts = appDBContent.Contract.ToList();
            contracts = contracts.OrderBy(i => i.clientID).ToList();
            return View("Index", contracts);
        }

        public ActionResult EmployeeFilter()
        {
            List<Contract> contracts = new List<Contract>();
            contracts = appDBContent.Contract.ToList();
            contracts = contracts.OrderBy(i => i.employeeID).ToList();
            return View("Index", contracts);
        }


        public ActionResult ClientFilter1()
        {
            List<Contract> contracts = new List<Contract>();
            contracts = appDBContent.Contract.ToList();
            contracts = contracts.OrderBy(i => i.clientID).ToList();
            return View("EmployeeViewsContract", contracts);
        }

        public ActionResult EmployeeFilter1()
        {
            List<Contract> contracts = new List<Contract>();
            contracts = appDBContent.Contract.ToList();
            contracts = contracts.OrderBy(i => i.employeeID).ToList();
            return View("EmployeeViewsContract", contracts);
        }


    }
}