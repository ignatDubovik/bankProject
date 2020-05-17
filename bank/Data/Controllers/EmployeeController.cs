using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bank.Data.Interfaces;
using bank.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace bank.Data.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployees _allEmployees;
        private readonly AppDBContent appDBContent;
        //public bool isAdmin = false;
        //public bool isUserAdmin=false;
        //public bool isUserAuthorized = false;
        //public string username="Гость";
        //public int userID=-1;
        public EmployeeController(IEmployees iemployee, AppDBContent apd)
        {
            _allEmployees = iemployee;
            this.appDBContent = apd;
        }

        public ViewResult EmployeeList()
        {
            var employees = _allEmployees.AllEmployees;
            return View(employees);
        }

        [HttpGet]
        public ActionResult Login()
        {
            RecordInSession("guest");
            bool isThereError = false;
            return View(isThereError);

        }
        [HttpPost]
        public ActionResult Verify(Employee emp)
        {
            //Employee emp = new Employee();

            emp = _allEmployees.logedEmployee(emp);
            if (emp.EmployeeLogin != "incorrect")
            {
                if (emp.isAdmin)
                {
                    //isAdmin = true;
                    RecordInSession("admin");
                    return View("AdminStartPage");
                    // this.RecordInSession("admin");
                    
                }
                else
                {
                    RecordInSession(emp.id.ToString());
                    return View("EmployeeStartPage");
                    //this.RecordInSession(emp.id.ToString());
                }

            }
            else
            {
                RecordInSession("guest");
                bool isThereError = true;
                return View("Login", isThereError);
            }
        }
        
        public ActionResult AdminPage()
        {
            return View("AdminStartPage");
        }

        public ActionResult EmployeePage()
        {
            return View("EmployeeStartPage");
        }

        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                using (appDBContent)
                {
                    return View(appDBContent.Employee.ToList());
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        [HttpPost]
        public ActionResult Index(Employee employee)
        {
            List<Employee> employees = new List<Employee>();
            employees = appDBContent.Employee.Where(x => x.EmployeeFullName.Contains(employee.EmployeeFullName)).ToList();
            if (employees != null)
            {
                return View("Index", employees);
            }
            else
            {
                return View("Index", ViewBag.Message = "Совпадений нет!");
            }
        }

        public ActionResult FIOFilter()
        {
            List<Employee> employees= new List<Employee>();
            employees=appDBContent.Employee.ToList();
            employees = employees.OrderBy(i=>i.EmployeeFullName).ToList();
            return View("Index", employees);
        }
        public ActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                using (appDBContent)
                {
                    return View(appDBContent.Employee.Where(x => x.id == id).FirstOrDefault());
                }
            }
            else return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                return View();
            }
            else return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                try
                {
                    using (appDBContent)
                    {
                        List<Employee> empl = new List<Employee>();

                        empl = appDBContent.Employee.Where(x => x.EmployeeLogin == emp.EmployeeLogin).ToList();
                        if (empl.Count == 0)
                        {
                            appDBContent.Employee.Add(emp);
                            appDBContent.SaveChanges();
                        }
                        else
                        {
                            ViewBag.Message = "Сотрудник уже зарегистрирован в системе!";
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
            else return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                using (appDBContent)
                {
                    return View(appDBContent.Employee.Where(x => x.id == id).FirstOrDefault());
                }
            }
            else return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult Edit(int id, Employee emp)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                try
                {
                    using (appDBContent)
                    {

                        List<Employee> empl = new List<Employee>();

                        empl = appDBContent.Employee.Where(x => x.EmployeeLogin == emp.EmployeeLogin).ToList();
                        if (empl.Count == 0)
                        {
                            emp.id = id;
                            emp.isAdmin = false;
                            appDBContent.Entry(emp).State = EntityState.Modified;
                            appDBContent.SaveChanges();
                        }
                        else
                        {
                            ViewBag.Message = "Сотрудник уже зарегистрирован в системе!";
                            return View();
                        }
                                                
                    }
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            else return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                using (appDBContent)
                {
                    return View(appDBContent.Employee.Where(x => x.id == id).FirstOrDefault());
                }
            }
            else return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult Delete(int id, Employee em)
        {
            if (HttpContext.Session.GetString("actions") == "admin")
            {
                try
                {
                    using (appDBContent)
                    {
                        Employee employee = appDBContent.Employee.Where(x => x.id == id).FirstOrDefault();
                        if (!employee.isAdmin)
                        {
                            appDBContent.Employee.Remove(employee);
                            appDBContent.SaveChanges();
                        }
                    }
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View("Delete");
                }
            }
            else return RedirectToAction("Login");
        }

        
        private void RecordInSession(string action)
        {
            var paths = HttpContext.Session.GetString("actions") ?? string.Empty;
            HttpContext.Session.SetString("actions", action);
        }


        public ActionResult EmployeePersonalPage()
        {
            if (!(HttpContext.Session.GetString("actions") == "admin" || HttpContext.Session.GetString("actions") == "guest"))
            {
                int EmployeeID = int.Parse(HttpContext.Session.GetString("actions"));
                using (appDBContent)
                {
                    return View(appDBContent.Employee.Where(x => x.id == EmployeeID).FirstOrDefault());
                }
            }
            else
            {
                //return RedirectToAction("Employee/Login");
                return RedirectToRoute(new { controller = "Employee", action = "Login" });
            }
        }

        public ActionResult MapPage()
        {
            if (!(HttpContext.Session.GetString("actions") == "admin" || HttpContext.Session.GetString("actions") == "guest"))
            {
                return View();
            }
            else
            {
                //return RedirectToAction("Employee/Login");
                return RedirectToRoute(new { controller = "Employee", action = "Login" });
            }
            
        }

    }
}