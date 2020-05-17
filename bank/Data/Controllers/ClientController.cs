using bank.Data.Interfaces;
using bank.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank.Data.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClients _allClients;
        private readonly AppDBContent appDBContent;
        public ClientController(IClients iclients, AppDBContent apd)
        {
            _allClients = iclients;
            this.appDBContent = apd;
        }

        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                using (appDBContent)
                {
                    return View(appDBContent.Client.ToList());
                    //return View(appDBContent.Client.Where(x=>x.adress.Contains("ерж")).ToList());
                }
            }
            else
            {
                //return RedirectToAction("Employee/Login");
                return RedirectToRoute(new { controller = "Employee", action = "Login" });
            }
        }

        [HttpPost]
        public ActionResult Index(Client client)
        {
            List<Client> clients = new List<Client>();
            clients = appDBContent.Client.Where(x => x.fullname.Contains(client.fullname)).ToList();
            if(clients!=null)
            {
                return View("Index", clients);
            }
            else
            {
                return View("Index", ViewBag.Message = "Совпадений нет!");
            }
        }



        public ActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                using (appDBContent)
                {
                    return View(appDBContent.Client.Where(x => x.id == id).FirstOrDefault());
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
        public ActionResult Create(Client client)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                try
                {
                    using (appDBContent)
                    {
                        List<Client> cl = new List<Client>();

                        cl = appDBContent.Client.Where(x => x.passport == client.passport).ToList();
                        if (cl.Count == 0)
                        {
                            appDBContent.Client.Add(client);
                            appDBContent.SaveChanges();
                        }
                        else
                        {
                            ViewBag.Message = "Клиент уже зарегистрирован в системе!";
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
                    return View(appDBContent.Client.Where(x => x.id == id).FirstOrDefault());
                }
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }
        [HttpPost]
        public ActionResult Edit(int id, Client client)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                try
                {
                    using (appDBContent)
                    {
                        client.id = id;
                        appDBContent.Entry(client).State = EntityState.Modified;
                        appDBContent.SaveChanges();
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
                    return View(appDBContent.Client.Where(x => x.id == id).FirstOrDefault());
                }
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }
        [HttpPost]
        public ActionResult Delete(int id, Client cl)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                try
                {
                    using (appDBContent)
                    {
                        Client client = appDBContent.Client.Where(x => x.id == id).FirstOrDefault();
                        
                            appDBContent.Client.Remove(client);
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


        public ActionResult EmployeeViewClients()
        {
            if (!(HttpContext.Session.GetString("actions") == "admin" || HttpContext.Session.GetString("actions") == "guest"))
            {
                int EmployeeID = int.Parse(HttpContext.Session.GetString("actions"));
                using (appDBContent)
                {
                    return View(appDBContent.Client.ToList());
                }
            }
            else
            {
                //return RedirectToAction("Employee/Login");
                return RedirectToRoute(new { controller = "Employee", action = "Login" });
            }
        }

        [HttpPost]
        public ActionResult EmployeeViewClients(Client client)
        {
            List<Client> clients = new List<Client>();
            clients = appDBContent.Client.Where(x => x.fullname.Contains(client.fullname)).ToList();
            if (clients != null)
            {
                return View("EmployeeViewClients", clients);
            }
            else
            {
                return View("EmployeeViewClients", ViewBag.Message = "Совпадений нет!");
            }
        }


        [HttpGet]
        public ActionResult CreateClientByEmployee()
        {
            if (!(HttpContext.Session.GetString("actions") == "admin" || HttpContext.Session.GetString("actions") == "guest"))
            {
                return View();
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }
        [HttpPost]
        public ActionResult CreateClientByEmployee(Client client)
        {
            if (!(HttpContext.Session.GetString("actions") == "admin" || HttpContext.Session.GetString("actions") == "guest"))
            {
                try
                {
                    using (appDBContent)
                    {
                        List<Client> cl = new List<Client>();

                        cl = appDBContent.Client.Where(x => x.passport == client.passport).ToList();
                        if (cl.Count == 0)
                        {
                            appDBContent.Client.Add(client);
                            appDBContent.SaveChanges();
                        }
                        else
                        {
                            ViewBag.Message = "Клиент уже зарегистрирован в системе!";
                            return View();
                        }
                    }
                    return RedirectToAction("EmployeeViewClients");
                }
                catch
                {
                    return View();
                    //return RedirectToAction("Index");
                }
            }
            else return RedirectToRoute(new { controller = "Employee", action = "Login" });
        }


        public ActionResult FIOFilter()
        {
            List < Client> clients = new List<Client>();
            clients = appDBContent.Client.ToList();
            clients = clients.OrderBy(i => i.fullname).ToList();
            return View("Index", clients);
        }
        public ActionResult FIOFilter1()
        {
            List<Client> clients = new List<Client>();
            clients = appDBContent.Client.ToList();
            clients = clients.OrderBy(i => i.fullname).ToList();
            return View("EmployeeViewClients", clients);
        }


    }
}
