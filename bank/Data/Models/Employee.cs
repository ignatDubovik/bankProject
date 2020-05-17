using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank.Data.Models
{
    public class Employee
    {
        public int id { set; get; }
        public string EmployeeLogin { set; get; }
        public string EmployeePassword { set; get; }
        public string EmployeeFullName { set; get; }
        public string EmployeePhoto { set; get; }
        public bool isAdmin { set; get; }
        public List<Contract> contracts { set; get; }
    }
}
